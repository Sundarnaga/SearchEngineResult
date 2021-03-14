using SearchEngineResults.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchEngineResults.Api.Logics
{
    public interface ISearchEngineLogic
    {
        List<string> GetSearchEngine();
        Task<IEnumerable<SearchResult>> GetSearchResults(List<SearchParameter> parameters, IHttpClientFactory clientFactory);
    }
}