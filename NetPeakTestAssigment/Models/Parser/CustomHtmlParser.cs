using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.Parser
{
    public class CustomHtmlParser : IHtmlParser
    {
        public SiteDetails ParseHTML(string html)
        {
            //TODO: own implementation of html parser
            //Take care of endless tags: meta, link, img, br
            //or tags that doesnt require end '/': li
            //of unquoted or empty values of attributes
            //etc.
            throw new NotImplementedException();
        }
    }
}
