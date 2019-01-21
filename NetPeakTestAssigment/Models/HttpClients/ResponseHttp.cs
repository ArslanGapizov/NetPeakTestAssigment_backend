using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.HttpClients
{
    public class ResponseHttp
    {
        public long TimeToFirstByte { get; set; }
        public ResponseStatus Status { get; set; }
        public string Body { get; set; }
        public string ResponseUri { get; set; }
    }
}
