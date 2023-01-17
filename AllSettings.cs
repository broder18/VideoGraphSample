using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;

namespace BIONVideoPlayer
{
    public static class AllSettings
    {
        /* *.ini file values */
        public static Rectangle MainForm;
        public static readonly Rectangle[] Renderers;
        public static int TelemetryPosX;
        public static int TelemetryPosY;
        public static int TelemetryAlpha;
        public static uint TelemetryTxtColor;
        public static uint TelemetryBkgColor;
        public static int EnableTelemetry;


        /* *.ini file keys */
        private const string MainFormName = "MainForm";
        private const string TelemetryPosXName = "TelemetryX";
        private const string TelemetryPosYName = "TelemetryY";
        private const string TelemetryAlphaName = "TelemetryAlpha";
        private const string RendererName = "Renderer";
        private const string TelemetryTxtColorName = "TelemetryText";
        private const string TelemetryBkgColorName = "TelemetryBkg";
        private const string EnableTelemetryName = "EnableTelemetry";

        static AllSettings()
        {
            Renderers = new Rectangle[Defines.MaxChannels];
            Load();
        }

        private static void Load()
        {
            var ini = new IniFile();
            TelemetryPosX = ini.ReadInt(TelemetryPosXName, Defines.TelemetryPosX, -Defines.VideoW, Defines.VideoW);
            TelemetryPosY = ini.ReadInt(TelemetryPosYName, Defines.TelemetryPosY, -Defines.VideoH, Defines.VideoH);
            TelemetryAlpha = ini.ReadInt(TelemetryAlphaName, Defines.TelemetryAlpha, 0, 100);
            TelemetryTxtColor = (uint)ini.ReadInt(TelemetryTxtColorName, Defines.TelemetryTxtColor) & 0x00ffffff;
            TelemetryBkgColor = (uint)ini.ReadInt(TelemetryBkgColorName, Defines.TelemetryBkgColor) & 0x00ffffff;
            EnableTelemetry = ini.ReadInt(EnableTelemetryName, Defines.EnableTelemetry);

            MainForm = StringToRect(ini.ReadString(MainFormName, ""), 0);
            for (int i = 0; i < Defines.MaxChannels; i++) Renderers[i] = StringToRect(ini.ReadString(RendererName + i, ""), i);
        }

        public static void Save()
        {
            var ini = new IniFile();

            try
            {
                ini.WriteInt(TelemetryPosXName, TelemetryPosX);
                ini.WriteInt(TelemetryPosYName, TelemetryPosY);
                ini.WriteInt(TelemetryAlphaName, TelemetryAlpha);
                ini.WriteInt(TelemetryTxtColorName, (int)TelemetryTxtColor);
                ini.WriteInt(TelemetryBkgColorName, (int)TelemetryBkgColor);
                ini.WriteInt(EnableTelemetryName, EnableTelemetry);

                ini.WriteString(MainFormName, RectToString(MainForm));
                for (int i = 0; i < Defines.MaxChannels; i++) ini.WriteString(RendererName + i, RectToString(Renderers[i]));

            }
            catch (Exception e)
            {
                Utils.ErrorBox($"Could not save settings to {ini.IniPath}\r\n\r\n" + e.Message);
            }
        }

        private static string RectToString(Rectangle r)
        {
            return new RectangleConverter().ConvertToString(r);
        }

        private static Rectangle StringToRect(string s, int i)
        {
            if (!string.IsNullOrEmpty(s))
            {
                try
                {
                    object obj = new RectangleConverter().ConvertFromString(s);
                    if (obj == null) throw new Exception();
                    return (Rectangle)obj;
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch { }
            }

            return new Rectangle(i * Defines.CascadeOffsetX, i * Defines.CascadeOffsetY, Defines.DefRendererW, Defines.DefRendererH);
        }

//        private static string PidsToString(int[] arr, int[] def)
//        {
//            /* writing null string to *.ini file deletes the key */
//            if (arr == null || arr.SequenceEqual(def)) return null;
//
//            var sb = new StringBuilder();
//            foreach (int value in arr) sb.Append($"{(ushort)value:X04}");
//            return sb.ToString();
//        }

/*
        private static int[] StringToPids(string str, int[] def)
        {
            if (string.IsNullOrEmpty(str) || str.Length % 4 != 0) return def;

            int cnt = str.Length / 4;
            if (cnt > Defines.MaxChannels) return def;

            int[] arr = new int[cnt];

            for (int i = 0; i < cnt; i++)
            {
                string tmp = str.Substring(i * 4, 4);
                if (!int.TryParse(tmp, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out arr[i])) return def;
            }

            return arr;
        }
*/

    }


}
