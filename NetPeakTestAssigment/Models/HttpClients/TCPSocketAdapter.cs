using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.HttpClients
{
    public class TCPSocketAdapter : IHttpClient
    {
        public Task<ResponseHttp> SendAsync(string url)
        {
            throw new NotImplementedException();
        }
    }
}
