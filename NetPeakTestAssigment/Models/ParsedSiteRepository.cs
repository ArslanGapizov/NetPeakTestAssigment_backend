using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models
{
    public class ParsedSiteRepository : IParsedSitesRepository
    {
        private static List<ParsedSiteDTO> _parsedSites
            = new List<ParsedSiteDTO>();

        public ParsedSiteRepository()
        {
        }
        private static int _indexer = 1;

        public void Add(ParsedSiteDTO value)
        {
            value.Id = _indexer++;
            _parsedSites.Add(value);
        }

        public void DeleteById(int id)
        {
            _parsedSites.RemoveAll(i => i.Id == id);
        }

        public ParsedSiteDTO GetById(int id)
        {
            return _parsedSites.FirstOrDefault(i => i.Id == id);
        }

        public ParsedSiteDTO GetByUri(string uri)
        {
            return _parsedSites.FirstOrDefault(i => i.Uri == uri);
        }

        public IEnumerable<ParsedSiteDTO> GetAll()
        {
            return _parsedSites;
        }

    }
}
