using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.HttpClients
{
    public class HttpClientAdapter : IHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;
        public HttpClientAdapter(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<ResponseHttp> SendAsync(string url)
        {
            HttpRequestMessage requestHttp = new HttpRequestMessage(HttpMethod.Get,
                                                 url);
            HttpClient client = _clientFactory.CreateClient();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            HttpResponseMessage responseHttp;
            try
            {
                
                responseHttp = await client.SendAsync(requestHttp, HttpCompletionOption.ResponseHeadersRead);
                sw.Stop();
            }
            catch (HttpRequestException e)
            {
                return null;
            }
            finally
            {
                /*if (sw.IsRunning)
                    sw.Stop();*/
            }

            return new ResponseHttp
            {
                ResponseUri = responseHttp.RequestMessage.RequestUri.ToString(),
                Body = await responseHttp.Content.ReadAsStringAsync(),
                TimeToFirstByte = sw.ElapsedMilliseconds,
                Status = new ResponseStatus
                {
                    Code = (int)responseHttp.StatusCode,
                    Description = responseHttp.StatusCode.ToString()
                }
            };
        }
    }
}
