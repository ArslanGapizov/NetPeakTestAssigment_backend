using NetPeakTestAssigment.Models.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models
{
    public class ParsedSiteDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ServerResponseDTO ServerResponse { get; set; }
        public long ResponseTime { get; set; }
        public IEnumerable<string> Headers { get; set; }
        public IEnumerable<HTMLImageDTO> Images { get; set; }
        public IEnumerable<HTMLLinkDTO> Links { get; set; }
    }

    public enum LinkType
    {
        INTERNAL = 0,
        EXTERNAL = 1
    }
}
