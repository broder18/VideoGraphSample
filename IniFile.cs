using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BIONVideoPlayer
{
    public class IniFile
    {
        public readonly string IniPath;
        private const string Section = "Settings";

        public IniFile()
        {
            IniPath = Path.ChangeExtension(Application.ExecutablePath, ".ini");
        }

        public int ReadInt(string name, int def, int min = int.MinValue, int max = int.MaxValue)
        {
            int val = (int) NativeMethods.GetPrivateProfileInt(Section, name, def, IniPath);
            if (val < min) return min;
            return val > max ? max : val;
        }

        public void WriteString(string name, string val)
        {
            NativeMethods.WritePrivateProfileString(Section, name, val, IniPath);
        }

        public void WriteInt(string name, int val)
        {
            WriteString(name, val.ToString());
        }

        public string ReadString(string name, string def)
        {
            var sb = new StringBuilder(1024);
            NativeMethods.GetPrivateProfileString(Section, name, "", sb, (uint) sb.Capacity, IniPath);
            string result = sb.ToString();
            return string.IsNullOrEmpty(result) ? def : result;
        }
    }
}
