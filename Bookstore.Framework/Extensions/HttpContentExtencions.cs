using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bookstore.Framework.Test
{
    public static class HttpContentExtencions
    {
        public static Task<TObj> ReadAsObjectAsync<TObj>(this HttpContent httpContent)
        {
            return Task.Run(async () =>
            {
                string responseContent = await httpContent.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseContent))
                    return default;

                return JToken
                    .Parse(responseContent)
                    .ToObject<TObj>();
            });
        }
    }
}
