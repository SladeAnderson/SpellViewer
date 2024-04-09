using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpellViewer.Tools
{
    public class JsonTools : IJsonTools
    {   
        private string path = GetExecutingDirectory().ToString();

        public JsonTools()
        {
            
        }

        public T? GetJson<T>(string name){
            using (StreamReader r = new StreamReader($"{path}/data/{name}.json"))
            {
                string json = r.ReadToEnd();
                var item = JsonConvert.DeserializeObject<T>(json);
                return item;
            }
        }


        public static DirectoryInfo GetExecutingDirectory()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).Directory.Parent.Parent.Parent;
        }
    }

    public interface IJsonTools
    {
        public T? GetJson<T>(string name);
    }
}