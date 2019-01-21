using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.Parser
{
    public class SiteDetails
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<string> HeadersH1 { get; set; }
        public List<string> Links { get; set; }
        public List<HTMLImage> Images { get; set; }
    }
}
