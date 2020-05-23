using System.IO;
using System.Runtime.Serialization.Json;

namespace XsmService.Utils
{
    public static class JsonUtil
    {
        public static string ToJsonString<T>(this T o)
        {
            return JsonUtil<T>.ObjToStr(o);
        }
    }

    public class JsonUtil<T>
    {
        private static readonly DataContractJsonSerializer Ser = new DataContractJsonSerializer(typeof(T));

        public static string ObjToStr( T o)
        {
            MemoryStream stream1 = new MemoryStream();
            Ser.WriteObject(stream1, o);
            stream1.Position = 0;
            StreamReader reader = new StreamReader(stream1);
            string text = reader.ReadToEnd();
            return text;
        }

        public static T ObjFromStr(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return (T)Ser.ReadObject(stream);
        }
    }
}
