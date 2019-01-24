using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models
{
    public class HTMLLinkDTO
    {
        public string Href { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public LinkType LinkType { get; set; }
    }

    public enum LinkType
    {
        INTERNAL = 0,
        EXTERNAL = 1
    }
}
