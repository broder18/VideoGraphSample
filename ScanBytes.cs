using System.Collections.Generic;
using System.IO;

namespace BIONVideoPlayer
{
    public static class ScanBytes
    {
        private const int M_BYTE = 1024 * 1024;
        private const byte SYNC_BYTE = 0x47;
        private const byte TP_SIZE = 188;
        private const byte PID_MASK = 0x1F;

        public static void SearchSyncByte(string path, ref Dictionary<ushort, bool> mapPids) 
        {
            using (var fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                fsSource.Seek(0, SeekOrigin.Begin);
                var byteArray = new byte[M_BYTE];
                var counter = 0;
                var bytesRead = fsSource.Read(byteArray, 0, M_BYTE);

                while (bytesRead != 0)
                {
                    if (!ReadByte(ref counter, bytesRead, ref byteArray, fsSource))
                    {
                        fsSource.Seek(counter, SeekOrigin.Begin);
                        bytesRead = fsSource.Read(byteArray, 0, M_BYTE);
                    }
                    else bytesRead = 0;
                }

                CheckPids(ref byteArray, ref mapPids);

            }
        }

        private static bool ReadByte(ref int counter, int bytesRead, ref byte[] bytes, Stream fsSource)
        {
            for (var idx = 0; idx < bytesRead; idx++)
            {
                if (bytes[idx] == SYNC_BYTE)
                {
                    fsSource.Seek(counter, SeekOrigin.Begin);
                    bytesRead = fsSource.Read(bytes, 0, M_BYTE);
                    if (IsValidPointer(ref bytes, 0)) return true;
                    idx = 0;
                }
                counter++;
            }

            return false;
        }

        private static bool IsValidPointer(ref byte[] bytes, int idX)
        {
            var idX2 = idX + TP_SIZE;
            var idX3 = idX + TP_SIZE;
            return bytes[idX] == SYNC_BYTE && bytes[idX2] == SYNC_BYTE && bytes[idX3] == SYNC_BYTE;
        }

        private static void CheckPids(ref byte[] bytes, ref Dictionary<ushort, bool> mapPids)
        {
            for (var idx = 0; idx < bytes.Length; idx += 188)
            {
                var croppedByte = (byte)(bytes[idx + 1] & PID_MASK);
                var res = (ushort)(croppedByte * 256 + bytes[idx + 2]);
                if (mapPids.ContainsKey(res)) mapPids[res] = true;
            }
        }

    }
}
