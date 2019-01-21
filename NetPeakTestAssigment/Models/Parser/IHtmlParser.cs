using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.Parser
{
    public interface IHtmlParser
    {
        SiteDetails ParseHTML(string html);
    }
}
