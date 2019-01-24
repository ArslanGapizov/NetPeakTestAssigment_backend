using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetPeakTestAssigment.Models
{
    //Store history of parse and upload requests
    public interface IParsedSitesRepository
    {
        IEnumerable<ParsedSiteDTO> GetAll();
        ParsedSiteDTO GetByUri(string uri);
        ParsedSiteDTO GetById(int id);
        void Add(ParsedSiteDTO value);
        void DeleteById(int id);
    }
}
