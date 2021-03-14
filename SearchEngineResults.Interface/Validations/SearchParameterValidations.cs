using SearchEngineResults.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SearchEngineResults.Interface.Validations
{

    public class SearchParameterValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                var searchParameter = value as List<SearchParameter>;
                foreach (SearchParameter parameter in searchParameter)
                {
                    if (parameter.SearchEngine == string.Empty)
                    {
                        return new ValidationResult("Search Engine can't be empty");
                    }
                    if (parameter.Keywords.Length == 0)
                    {
                        return new ValidationResult($"Keyword can't be empty for the search engine {parameter.SearchEngine}");
                    }
                    foreach(string keyword in parameter.Keywords)
                    {
                        if (string.IsNullOrEmpty(keyword))
                        {
                            return new ValidationResult($"Keyword can't be empty for the search engine {parameter.SearchEngine}");
                        }
                    }
                }
            }
            return ValidationResult.Success;

        }

    }
}
