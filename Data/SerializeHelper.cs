using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data
{
    public static class SerializeHelper
    {
        public static string Serialize(dynamic obj)
        {
            return JsonConvert.SerializeObject(obj);

        }
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);

        }
    }
}
