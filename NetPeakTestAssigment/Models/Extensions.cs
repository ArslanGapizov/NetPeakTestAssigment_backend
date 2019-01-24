using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models
{
    public static class Extensions
    {
        //From relative to absolute links, also mark links external and internal relative to domain
        public static IEnumerable<HTMLLinkDTO> RelativeToAbsoluteLinks(this IEnumerable<string> links, string domain)
        {
            Uri domainUri = new Uri(domain);

            Uri result = null;
            foreach (var link in links)
            {
                LinkType linkType = LinkType.EXTERNAL;
                Uri.TryCreate(domainUri, link, out result);
                if (domainUri.Host == result?.Host)
                    linkType = LinkType.INTERNAL;

                yield return new HTMLLinkDTO
                {
                    Href = result?.AbsoluteUri ?? link,
                    LinkType = linkType
                };
            }
        }
        public static IEnumerable<HTMLImageDTO> imageLinksToAbsoluteLinks(this IEnumerable<Parser.HTMLImage> imgs, string domain)
        {
            Uri domainUri = new Uri(domain);
            Uri result = null;
            foreach (var img in imgs)
            {
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
