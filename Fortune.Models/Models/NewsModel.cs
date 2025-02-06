using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Models.Models
{
    public class NewsModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PubDate { get; set; }

        public string ToSummary()
        {
            return $"Title: {Title}, Description: {Description}, Date: {PubDate.ToString("yyyy-MM-dd")}";
        }
    }
}