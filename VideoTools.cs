using Newtonsoft.Json;

using System;
using System.Diagnostics;
using System.Linq;

using TagLib;

//namespace Plugin_VideoTools
//{
public class JSON
{
    public object MimeType { get; set; }
    public object Properties { get; set; }
    public object Tag { get; set; }
    public object Error { get; set; }

}

public static class VideoTools
{
    public static string GetMetaDataProperties(string file, bool debug = false)
    {
        if (debug) Debugger.Launch();
        File meta = File.Create(file);
        object properties = meta.Properties;
        object mimetype = meta.MimeType;
        object error = null;
        object tag;
        try
        {
            tag = meta.Tag;
        }
        catch (Exception e)
        {
            error = e.Message;
            tag = null;
        }
        return JsonConvert.SerializeObject(new JSON() { Properties = properties, MimeType = mimetype, Tag = tag, Error = error });
    }

    public static string DetectVideoFormat(string file, bool debug = false)
    {
        if (debug) Debugger.Launch();

        var stream = System.IO.File.OpenRead(file);
        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, (int)stream.Length);
        stream.Close();
        var firstFour = bytes.Take(4).ToArray();
        var eightAfterFour = bytes.Skip(4).Take(8).ToArray();
        if (eightAfterFour.SequenceEqual<byte>([0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x41, 0x20]))
        {
            return "m4a";
        }
        if (eightAfterFour.SequenceEqual<byte>([0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56, 0x20]))
        {
            return "m4v";
        }
        if (eightAfterFour.SequenceEqual<byte>([0x66, 0x74, 0x79, 0x70, 0x4D, 0x53, 0x4E, 0x56]))
        {
            return "mp4";
        }
        if (eightAfterFour.SequenceEqual<byte>([0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D]))
        {
            return "mp4";
        }
        if (eightAfterFour.SequenceEqual<byte>([0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32]))
        {
            return "m4v";
        }
        if (eightAfterFour.SequenceEqual<byte>([0x66, 0x74, 0x79, 0x70, 0x71, 0x74, 0x20, 0x20]))
        {
            return "mov";
        }
        if (firstFour.SequenceEqual<byte>([0x1A, 0x45, 0xDF, 0xA3]))
        {
            return "mkv";
        }
        if (firstFour.SequenceEqual<byte>([0xFF, 0xD8, 0xFF, 0xE0]))
        {
            return "jpg";
        }
        if (firstFour.SequenceEqual<byte>([0xFF, 0xD8, 0xFF, 0xE1]))
        {
            return "jpg";
        }
        if (firstFour.SequenceEqual<byte>([0xFF, 0xD8, 0xFF, 0xE8]))
        {
            return "jpg";
        }
        if (firstFour.SequenceEqual<byte>([0xFF, 0xD8, 0xFF, 0xFE]))
        {
            return "jpg";
        }
        return "$$$";
    }
}
//}
