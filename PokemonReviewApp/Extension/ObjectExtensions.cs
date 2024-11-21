using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;

namespace PokemonReviewApp.Extension
{
    public static class ObjectExtensions
    {
        public static object? ToGenericObject(this object? data)
        {
            if (data == null)
                return null;

            Type t = data.GetType();
            if (t.IsPrimitive || t.IsValueType || t == typeof(string))
            {
                return data;
            }
            else if (t.IsArray || IsList(data) || data is JArray)
            {
                var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                var deserialize = JsonConvert.DeserializeObject<IList<object?>>(json, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                IList<object> list = new List<object>();
                foreach (var obj in deserialize!)
                {
                    list.Add(obj.ToGenericObject()!);
                }
                return list;
            }
            else if (t.IsTypeDefinition || IsDictionary(data))
            {
                var json = JsonConvert.SerializeObject(data);
                var deserialize = JsonConvert.DeserializeObject<Dictionary<string, object?>>(json);
                Dictionary<string, object?> dict = new Dictionary<string, object?>();
                foreach (var key in deserialize!.Keys)
                {
                    dict[key.ToSnakeCase()!] = deserialize[key].ToGenericObject();
                }
                return dict;
            }
            return data;
        }

        public static bool IsList(object? o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsDictionary(object? o)
        {
            if (o == null) return false;
            return o is IDictionary &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }
    }
}
