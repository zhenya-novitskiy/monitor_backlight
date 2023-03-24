using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;


namespace Lightless
{
    public static class Configuration
    {
        private static readonly Dictionary<string, object> Data;

        static Configuration()
        {
            Data = Load();
        }

        static void Save(Dictionary<string, object> data)
        {
            var fs = new FileStream("Configuration.xml", FileMode.Create);
            var sw = new StreamWriter(fs);
            sw.Write(data.Serialize());
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        static Dictionary<string, object> Load()
        {
            var fs = new FileStream("Configuration.xml", FileMode.OpenOrCreate);
            var sr = new StreamReader(fs);
            var xml = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            if (String.IsNullOrEmpty(xml))
            {

                return new Dictionary<string, object>();
            }

            return xml.Deserialize<Dictionary<string, object>>();

        }
        public static bool Contains(params ConfigurationData[] keys)
        {
            return !(keys.Where(key => !Data.ContainsKey(key.ToString())).Count() > 0);
        }

        public static T Get<T>(ConfigurationData key)
        {
            return (T)Data[key.ToString()];
        }

        public static void Set(ConfigurationData key, object value)
        {
            if (Data.ContainsKey(key.ToString()))
            {
                Data[key.ToString()] = value;
            }
            else
            {
                Data.Add(key.ToString(), value);
            }
            Save(Data);
        }
    }


    public enum ConfigurationData
    {
        PortNumber,
        VolumeLevel,
        BaudRate,
        CurrentTheme
    }

    public static class SerializationExtensions
    {
        public static string Serialize<T>(this T obj)
        {
            var serializer = new DataContractSerializer(obj.GetType());
            using (var writer = new StringWriter())
            using (var stm = new XmlTextWriter(writer))
            {
                serializer.WriteObject(stm, obj);
                return writer.ToString();
            }
        }
        public static T Deserialize<T>(this string serialized)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var reader = new StringReader(serialized))
            using (var stm = new XmlTextReader(reader))
            {
                return (T)serializer.ReadObject(stm);
            }
        }
    }
}
