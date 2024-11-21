using PokemonReviewApp.Common.DataGram;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using PokemonReviewApp.Extension;

namespace PokemonReviewApp
{
    public static class ResponseExtensions
    {
        public static ResponseBase SetDataWith<TData>(this ResponseBase response, TData data) where TData : new()
        {
            var json = JsonConvert.SerializeObject(data);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (dict != null)
            {
                foreach(string key in dict.Keys)
                {
                    SetDataWith(response, key, dict[key]);
                }
            }
            return response;
        }

        public static T Success<T>(this T response, string? message = "", ResponseStatus status = ResponseStatus.Success) where T : ResponseBase
        {
            response.Status = status;
            if (!string.IsNullOrEmpty(message))
            {
                response.Message = message;
            }
            response.Success = true;
            return response;
        }

        public static T SetDataWith<T>(this T response, string key, object? data) where T : ResponseBase
        {
            return response.SetToData(key, data);
        }

        private static T SetToData<T>(this T response, string keyName, object? data) where T : ResponseBase
        {
            response.AddTempData(keyName.ToSnakeCase()!, data.ToGenericObject());
            return response;
        }
    }
}
