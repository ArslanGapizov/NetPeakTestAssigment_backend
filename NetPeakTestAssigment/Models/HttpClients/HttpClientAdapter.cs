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
        public async Task<ResponseHttp> SendAsync(Uri uri)
        {
            HttpRequestMessage requestHttp = new HttpRequestMessage(HttpMethod.Get,
                                                 uri.AbsoluteUri);

            HttpClient client = _clientFactory.CreateClient();
            
            HttpResponseMessage responseHttp;
            //Measuring TTFB
            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                responseHttp = await client.SendAsync(requestHttp, HttpCompletionOption.ResponseHeadersRead);
                //Not exactly TTFB, it is a time when headers is available, but content is not
                sw.Stop();
            }
            //Catch error, for example in case of wrong Uri
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            finally
            {
                if (sw.IsRunning)
                    sw.Stop();
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
