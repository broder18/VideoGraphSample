
namespace VideoGraphSample
{
    public static class Defines
    {
        public const int MaxChannels = 8;
        public const int VideoW = 960;
        public const int VideoH = 960;
        public const int GridSize = 3;

        public const int DefRendererW = VideoW / 3;
        public const int DefRendererH = VideoH / 3;

        public const int MinWindowW = 240;
        public const int MinWindowH = 240;

        public const int CascadeOffsetX = 40;
        public const int CascadeOffsetY = 70;

        public const int TelemetryPosX = 0;
        public const int TelemetryPosY = 0;
        public const int TelemetryAlpha = 100;
        public const int TelemetryTxtColor = 0x00ffffff;
        public const int TelemetryBkgColor = 0x00000000;
        public const int EnableTelemetry = (1 << MaxChannels) - 1;

        public static readonly int[] Pids = { 0x85, 0x86, 0x87, 0x88, 0x89 };
        public static readonly int[] Pmts = { 0x44, 0x45, 0x46, 0x47, 0x48 };
    }
}
