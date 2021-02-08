using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogPost.Common
{
    public static class Extension
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static void Set<T>(this ITempDataDictionary tempData, string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            tempData.Add(key, json);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key)
        {
            if (!tempData.ContainsKey(key)) return default(T);

            var value = tempData[key] as string;

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
