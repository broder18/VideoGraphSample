using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace VideoGraphSample
{
    public static class Utils
    {
        public static bool GetZOrderedForms(ref IntPtr[] forms)
        {
            int numforms = forms.Length;
            /* do we have anything to sort? */
            if (numforms < 2) return true;

            /* temprary array is twice as big as the original one because we can move in both directions */
            var temparray = new IntPtr[numforms * 2 - 1];
            int lowestidx = numforms - 1;
            /* put the very first handle in the middle */
            var curhwnd = forms[0];
            temparray[lowestidx] = curhwnd;

            var hwnds = new List<IntPtr>();
            for (int i = 1; i < numforms; i++) hwnds.Add(forms[i]);

            /* find all forms that are below the first one */
            while (hwnds.Count != 0)
            {
                curhwnd = NativeMethods.GetWindow(curhwnd, NativeMethods.GwHwndnext);
                if (curhwnd == IntPtr.Zero) break;

                /* is the window we have just found one of our windows? */
                int idx = hwnds.IndexOf(curhwnd);
                if (idx != -1)
                {
                    /* yeah! remove it from the list and add to the temporary array */
                    hwnds.RemoveAt(idx);
                    lowestidx--;
                    temparray[lowestidx] = curhwnd;
                }
            }

            curhwnd = forms[0];
            int biggestidx = numforms;

            /* find all forms that are above the the first one */
            while (hwnds.Count != 0)
            {
                curhwnd = NativeMethods.GetWindow(curhwnd, NativeMethods.GwHwndprev);
                if (curhwnd == IntPtr.Zero) break;

                /* is the window we have just found one of our windows? */
                int idx = hwnds.IndexOf(curhwnd);
                if (idx != -1)
                {
                    /* yeah! remove it from the list and add to the temporary array */
                    hwnds.RemoveAt(idx);
                    temparray[biggestidx] = curhwnd;
                    biggestidx++;
                }
            }

            /* have we found all our windows? */
            if (hwnds.Count != 0) return false;

            Array.Copy(temparray, lowestidx, forms, 0, numforms);
            return true;
        }

        public static void ErrorBox(string message)
        {
            MessageBox.Show(message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
