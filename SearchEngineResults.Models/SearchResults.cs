using System;
using System.Collections.Generic;
using System.Text;


namespace SearchEngineResults.Models
{
    public class SearchResults
    {
        public List<SearchResult> Results { get; set; } = new List<SearchResult>();
    }

    public class SearchResult
    {
        public string SearchEngine { get; set; }
        public string KeyWord { get; set; }
        public List<int> Results { get; set; } = new List<int>();
    }
}
