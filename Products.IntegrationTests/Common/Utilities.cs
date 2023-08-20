using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.IntegrationTests.Common
{
    internal static class Utilities
    {
        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static async Task<string> CreateRequestContent(object o)
        {
            var result = JsonConvert.SerializeObject(o);

            return result;
        }
    }
}
