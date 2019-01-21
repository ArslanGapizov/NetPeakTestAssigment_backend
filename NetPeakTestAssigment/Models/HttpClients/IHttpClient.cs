using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.HttpClients
{
    public interface IHttpClient
    {
        Task<ResponseHttp> SendAsync(string url);
    }
}
