using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SearchEngineResults.Models
{
    public class SearchParameter
    {
        [Required]
        public string[] Keywords { get; set; }
        [Required]
        public string SearchEngine { get; set; }
        [Required]
        public string UrlToMatch { get; set; }
    }
}
