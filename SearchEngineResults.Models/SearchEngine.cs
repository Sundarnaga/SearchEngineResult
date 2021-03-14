using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngineResults.Models
{
    public class SearchEngine
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string FilterExpression { get; set; }
        public int Count { get; set; }
    }
}
