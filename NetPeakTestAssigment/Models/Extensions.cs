using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models
{
    public static class Extensions
    {
        public static IEnumerable<HTMLLinkDTO> BeautifyLinks(this IEnumerable<string> links, string domain)
        {
            Uri domainUri = new Uri(domain);
            foreach (var link in links)
            {
                Uri result = null;
                LinkType linkType = LinkType.EXTERNAL;

                Uri.TryCreate(domainUri, link, out result);
                if (domainUri.Host == result?.Host)
                {
                    linkType = LinkType.INTERNAL;
                }
                yield return new HTMLLinkDTO
                {
                    Href = result?.AbsoluteUri ?? link,
                    LinkType = linkType
                };
            }
        }
        public static IEnumerable<HTMLImageDTO> BeautifyImageLinks(this IEnumerable<Parser.HTMLImage> imgs, string domain)
        {
            Uri domainUri = new Uri(domain);
            foreach (var img in imgs)
            {
                Uri result = null;

                Uri.TryCreate(domainUri, img.Src, out result);
                yield return new HTMLImageDTO
                {
                    Src = result?.AbsoluteUri ?? img.Src,
                    Alt = img.Alt
                };
            }
        }
    }
}
