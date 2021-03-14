using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchEngineResults.Infra;
using Microsoft.Extensions.Configuration;
using SearchEngineResults.Api.Logics;
using SearchEngineResults.Models;
using SearchEngineResults.Interface;
using Microsoft.Extensions.Caching.Memory;
using SearchEngineResults.Api.Extensions;
using System.Net.Http;

namespace SearchEngineResults.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchEngineController : ControllerBase
    {

        private ILoggerManager logger;
        private readonly IConfiguration config;
        private readonly IHttpClientFactory clientFactory;
        private SearchEngineLogic searchEngineLogic;
        private IMemoryCache cache;

        public SearchEngineController(ILoggerManager logger, IConfiguration config, IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.config = config;
            this.cache = memoryCache;
            this.clientFactory = httpClientFactory;
            this.searchEngineLogic = new SearchEngineLogic(config, logger);
        }


        /// <summary>
        /// API Call to get the Search Engine list
        /// </summary>
        /// <returns>Returs list of search engine name</returns>
        [HttpGet]
        [Route("Engines")]
        public IActionResult GetEngines()
        {
            List<string> searchEngine = searchEngineLogic.GetSearchEngine();
            if (searchEngine.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(searchEngine);
            }
        }

        /// <summary>
        /// API Call to return the search results
        /// </summary>
        /// <param name="request">Request parameter with different search engine and parameters</param>
        /// <returns></returns>

        [HttpPost]
        [Route("SearchResults")]
        public async Task<IActionResult> PostSearchResults(SearchResultRequest request)
        {

            IEnumerable<SearchResult> searchResults = null;
            List<SearchParameter> searchParameterList = null;
            if (!cache.TryGetValue<List<SearchParameter>>(CONSTANTS.CACHE_SEARCHRESULTREQUESTKEY, out searchParameterList))
            {
                searchParameterList = request.SearchParameter;
                cache.Set<List<SearchParameter>>(CONSTANTS.CACHE_SEARCHRESULTREQUESTKEY, searchParameterList, CacheExpirySetting());
            }

            if(!searchParameterList.ScrambledEquals(request.SearchParameter))
            {
                searchParameterList = request.SearchParameter;
                cache.Set<List<SearchParameter>>(CONSTANTS.CACHE_SEARCHRESULTREQUESTKEY, searchParameterList, CacheExpirySetting());
                if (cache.TryGetValue<IEnumerable<SearchResult>>(CONSTANTS.CACHE_SEARCHRESULTRESPONSEKEY, out searchResults))
                {
                    cache.Remove(CONSTANTS.CACHE_SEARCHRESULTRESPONSEKEY);
                }
            }

            if (!cache.TryGetValue<IEnumerable<SearchResult>>(CONSTANTS.CACHE_SEARCHRESULTRESPONSEKEY, out searchResults))
            {
                searchResults = await searchEngineLogic.GetSearchResults(request.SearchParameter,clientFactory);              
                cache.Set<IEnumerable<SearchResult>>(CONSTANTS.CACHE_SEARCHRESULTRESPONSEKEY, searchResults, CacheExpirySetting());
            }

            return Ok(searchResults);

        }

        /// <summary>
        /// Method to set the in-memory cache option to 60 minutes
        /// </summary>
        /// <returns></returns>
        private MemoryCacheEntryOptions CacheExpirySetting()
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(config.GetValue<int>(CONSTANTS.CONFIG_CACHEEXPIRETIME));
            cacheExpirationOptions.Priority = CacheItemPriority.Normal;
            return cacheExpirationOptions;
        }


    }
}