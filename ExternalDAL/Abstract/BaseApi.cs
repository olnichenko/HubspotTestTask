using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDAL.Abstract
{
    public abstract class BaseApi : IApi
    {
        public async Task<string> GetJsonDataAsync(string url, Dictionary<string, string>? headers)
        {
            using (var httpClient = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    if (headers != null)
                    {
                        foreach (var item in headers)
                        {
                            requestMessage.Headers.Add(item.Key, item.Value);
                        }
                    }

                    var responseMessage = await httpClient.SendAsync(requestMessage);
                    var jsonResult = await responseMessage.Content.ReadAsStringAsync();
                    return jsonResult;
                }
            }
        }
    }
}
