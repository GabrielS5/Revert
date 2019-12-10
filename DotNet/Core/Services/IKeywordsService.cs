using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IKeywordsService
    {
        Task<IEnumerable<Keyword>> CollectKeywords(string text);
        IEnumerable<Keyword> ProcessKeywords(IEnumerable<Keyword> unprocessedKeywords, string name, string context);
    }
}
