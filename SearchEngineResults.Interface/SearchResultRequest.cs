using SearchEngineResults.Interface.Validations;
using SearchEngineResults.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SearchEngineResults.Interface
{
    public class SearchResultRequest
    {
        [Required]
        [SearchParameterValidation]
        public List<SearchParameter> SearchParameter { get; set; }
    }
}
