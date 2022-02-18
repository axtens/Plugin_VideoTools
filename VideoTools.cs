using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

//namespace Plugin_VideoTools
//{
public static class VideoTools
{
    public static string DetectVideoFormat(string file, bool debug = false)
    {
        if (debug) Debugger.Launch();

        var stream = File.OpenRead(file);
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, 12);
        stream.Close();
        var firstFour = bytes.Take(4).ToArray();
        var eightAfterFour = bytes.Skip(4).Take(8).ToArray();
        if (eightAfterFour.SequenceEqual<byte>(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41, 0x20 })) { 
            return "m4a";
        }
        if (eightAfterFour.SequenceEqual<byte>(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56, 0x20 }))
        {
            return "m4v";
        }
        if (eightAfterFour.SequenceEqual<byte>(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56 }))
        {
            return "mp4";
        }
        if (eightAfterFour.SequenceEqual<byte>(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D }))
        {
            return "mp4";
        }
        if (eightAfterFour.SequenceEqual<byte>(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 }))
        {
            return "m4v";
        }
        if (eightAfterFour.SequenceEqual<byte>(new byte[] { 0x66, 0x74, 0x79, 0x70, 0x71, 0x74, 0x20, 0x20 }))
        {
            return "mov";
        }
        if (firstFour.SequenceEqual<byte>(new byte[] { 0x1A, 0x45, 0xDF, 0xA3 }))
        {
            return "mkv";
        }
        return "$$$";
    }
}
//}
