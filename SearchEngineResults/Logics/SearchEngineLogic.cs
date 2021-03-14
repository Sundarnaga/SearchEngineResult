using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SearchEngineResults.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net;
using System.Text;
using SearchEngineResults.Infra;

namespace SearchEngineResults.Api.Logics
{
    public class SearchEngineLogic : ISearchEngineLogic
    {
        private IConfiguration _config;
        private ILoggerManager _logger;
        private List<SearchEngine> searchEngineList;

        public SearchEngineLogic(IConfiguration config, ILoggerManager logger)
        {
            this._config = config;
            this._logger = logger;
            searchEngineList = new List<SearchEngine>();
            _config.GetSection("SearchEngine").Bind(searchEngineList);
        }


        /// <summary>
        /// Get search engine list name from configuration
        /// </summary>
        /// <returns></returns>
        public List<string> GetSearchEngine()
        {
            return searchEngineList.Select(s => s.Name).ToList<string>();
        }


        /// <summary>
        /// Async execution of conneting the search engine server for getting the search results
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task<List<SearchResult>> connectAndGetResults(SearchParameter parameter, IHttpClientFactory clientFactory)
        {
            List<SearchResult> searchResults = new List<SearchResult>();
            var searchEngine = searchEngineList.Find(s => s.Name == parameter.SearchEngine);
            if (searchEngine != null)
            {
                SearchResult result;
                string sourceContent = string.Empty;
                foreach (string keyword in parameter.Keywords)
                {
                    string url = String.Format(searchEngine.Url, keyword, searchEngine.Count);
                    var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
                    request.Headers.Add("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
                    var client = clientFactory.CreateClient();
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        sourceContent = await response.Content.ReadAsStringAsync();
                        MatchCollection matches = Regex.Matches(sourceContent, searchEngine.FilterExpression, RegexOptions.Singleline);
                        if (matches.Count > 0)
                        {
                            result = new SearchResult();
                            result.SearchEngine = searchEngine.Name;
                            result.KeyWord = keyword;
                            for (int index = 0; index < matches.Count; index++)
                            {
                                if (matches[index].Value.Contains(parameter.UrlToMatch))
                                {
                                    result.Results.Add(index + 1);
                                }
                            }
                            searchResults.Add(result);
                        }
                        else
                        {
                            _logger.LogInfo($"No match found for the search engine url - {searchEngine.Url}");
                        }
                    }
                }
            }
            else
            {
                _logger.LogInfo($"No configurable search engine for the supplied parameter -{parameter.SearchEngine}");
            }
            return searchResults;
        }

        /// <summary>
        /// Method to create the tasks based on parameter and execute them in parallel to retrieve the results
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SearchResult>> GetSearchResults(List<SearchParameter> parameters, IHttpClientFactory clientFactory)
        {
            var tasks = new List<Task<List<SearchResult>>>();
            foreach (SearchParameter parameter in parameters)
            {
                tasks.Add(connectAndGetResults(parameter, clientFactory));
            }
            return (await Task.WhenAll(tasks)).SelectMany(u => u);
        }
    }
}

