using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Client.Utilities
{
    public static class HttpContentExtensions
    {
        public static async System.Threading.Tasks.Task<T> ReadAsObjectAsync<T>(this System.Net.Http.HttpContent content)
        {
            using (var stream = await content.ReadAsStreamAsync())
            using (var reader = new System.IO.StreamReader(stream))
            using (var json = new Newtonsoft.Json.JsonTextReader(reader))
            {
                return new Newtonsoft.Json.JsonSerializer().Deserialize<T>(json);
            }
        }
    }
}
