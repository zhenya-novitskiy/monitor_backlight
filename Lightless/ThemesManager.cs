using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lightless
{
    public static class ThemesManager
    {
        public static void Save(string themeName, IEnumerable<Pixel> data)
        {
            var fs = new FileStream("Themes/"+themeName +".xml", FileMode.Create);
            var sw = new StreamWriter(fs);
            sw.Write(data.ToList().Serialize());
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static List<Pixel> LoadTheme(string themeName)
        {
            var fs = new FileStream("Themes/"+ themeName +".xml", FileMode.OpenOrCreate);
            var sr = new StreamReader(fs);
            var xml = sr.ReadToEnd();

            sr.Close();
            fs.Close();

            return String.IsNullOrEmpty(xml) ? new List<Pixel>() : xml.Deserialize<List<Pixel>>();
        }
        public static void DeleteTheme(string themeName)
        {
            File.Delete("Themes/" + themeName + ".xml");
        }

        public static List<string> GetThemesList()
        {
            var result = new List<string>();
            
            DirectoryInfo dirInfo = new DirectoryInfo("Themes/");

            FileInfo[] info = dirInfo.GetFiles("*.xml");
            foreach (FileInfo f in info)
            {
                result.Add(Path.GetFileNameWithoutExtension(f.Name)); ;
            }

            return result;
        }

        public static string GetRandomThemeName()
        {
            string result = string.Empty;

            DirectoryInfo dirInfo = new DirectoryInfo("Themes/");

            FileInfo[] info = dirInfo.GetFiles("*.xml");

            var index = new Random().Next(info.Length);

            result = Path.GetFileNameWithoutExtension(info[index].Name);

            return result;
        }
    }
}
