using NetPeakTestAssigment.Models.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models
{
    //In memory store
    public class ParsedSiteDTO
    {
        public int Id { get; set; }
        public string Uri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ServerResponseDTO ServerResponse { get; set; }
        public long ResponseTime { get; set; }
        public IEnumerable<string> HeadersH1 { get; set; }
        public IEnumerable<HTMLImageDTO> Images { get; set; }
        public IEnumerable<HTMLLinkDTO> Links { get; set; }
    }
    
}
