using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace SearchEngineResults.Api.Extensions
{
    public static class CompareListExtensions
    {
        /// <summary>
        /// Comparing the two list by validation JSON object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            string myself = JsonConvert.SerializeObject(list1);
            string other = JsonConvert.SerializeObject(list2);
            return myself == other;
        }
    }
}
