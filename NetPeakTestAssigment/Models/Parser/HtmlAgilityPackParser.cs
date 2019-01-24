using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models.Parser
{
    public class HtmlAgilityPackParser : IHtmlParser
    {
        public SiteDetails ParseHTML(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                // TODO: Handle any parse errors if required
            }

            SiteDetails result = new SiteDetails
            {
                Title = GetTitle(htmlDoc),
                Description = GetDescription(htmlDoc),
                HeadersH1 = GetH1Content(htmlDoc),
                Links = GetLinks(htmlDoc),
                Images = GetHTMLImages(htmlDoc)
            };

            return result;
        }

        //Get paths from tags <img> with src
        private List<HTMLImage> GetHTMLImages(HtmlDocument htmlDoc)
        {
            List<HTMLImage> images = new List<HTMLImage>();
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//img[@src]");
            if (nodes == null)
                return images;
            foreach (HtmlNode node in nodes)
            {
                string imgSrc = node
                        .Attributes
                        .FirstOrDefault(x => x.Name == "src").Value;
                string imgAlt = node
                    .Attributes
                    .FirstOrDefault(x => x.Name == "alt")?.Value;

                images.Add(new HTMLImage
                {
                    Src = imgSrc,
                    Alt = imgAlt
                });
            }
            return images;
        }
        //Get paths from tags <a> with href
        private List<string> GetLinks(HtmlDocument htmlDoc)
        {
            List<string> links = new List<string>();
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
            if (nodes == null)
                return links;
            foreach (HtmlNode node in nodes)
            {
                string href = node
                        .Attributes
                        .FirstOrDefault(x => x.Name == "href").Value;

                links.Add(href);
            }
            return links;
        }
        //Get innerHTML from tags <h1>
        private List<string> GetH1Content(HtmlDocument htmlDoc)
        {
            List<string> headersH1 = new List<string>();
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//h1");
            if (nodes == null)
                return headersH1;
            foreach (HtmlNode node in nodes)
            {
                headersH1.Add(node.InnerHtml);
            }
            return headersH1;
        }
        //Get innerHTML from <title> tag
        private string GetTitle(HtmlDocument htmlDoc)
        {
            HtmlNode titleNode = htmlDoc.DocumentNode.SelectSingleNode("//title");
            return titleNode?.InnerHtml;
        }
        //Get content of meta tag with attribute name="description"
        private string GetDescription(HtmlDocument htmlDoc)
        {
            HtmlNode descriptionNode = htmlDoc.DocumentNode.SelectNodes("//meta")?
                .FirstOrDefault(x => x.Attributes
               .FirstOrDefault(y => y.Name == "name" && y.Value == "description") != null);
            
            return descriptionNode?.Attributes.FirstOrDefault(x => x.Name == "content").Value;
        }
    }
}
