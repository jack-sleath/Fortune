using Fortune.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Helpers
{
    public static class NewsSummaryHelper
    {
        public static List<NewsModel> PickRandomArticles(this List<NewsModel> list, int amountToPick = 1 )
        {
            return list.OrderBy(_ => Guid.NewGuid()) 
            .Take(amountToPick)          
            .ToList();
        }

        public static string GetSummaries(this List<NewsModel> list) 
        {
            return string.Join(", ", list.Select(m => m.ToSummary()));
        }
    }
}
