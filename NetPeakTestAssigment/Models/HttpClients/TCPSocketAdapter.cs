using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.HttpClients
{
    public class TCPSocketAdapter : IHttpClient
    {
        //Should be implemented using TCPSocket, if more accurate TTFB is required
        public async Task<ResponseHttp> SendAsync(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
