using Fortune.Models.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fortune.Helpers
{
    public static class BBCNewsFeed
    {
        private static readonly string _newsLink = "https://feeds.bbci.co.uk/news/world/rss.xml";
        public static List<NewsModel> GetNewsArticleSummaries()
        {
            try
            {
                string rssContent = FetchRssFeed();
                return ParseRssFeed(rssContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching or parsing the RSS feed: {ex.Message}");
                return new List<NewsModel>();
            }
        }

        private static string FetchRssFeed()
        {
            using WebClient client = new WebClient();
            return client.DownloadString(_newsLink);
        }

        private static List<NewsModel> ParseRssFeed(string rssContent)
        {
            List<NewsModel> items = new List<NewsModel>();
            XDocument xml = XDocument.Parse(rssContent);

            foreach (var item in xml.Descendants("item"))
            {
                string pubDateString = item.Element("pubDate")?.Value;
                DateTime pubDate = DateTime.MinValue; // Default if parsing fails

                if (!string.IsNullOrWhiteSpace(pubDateString))
                {
                    // Attempt to parse using RFC1123 format
                    if (!DateTime.TryParseExact(pubDateString, "ddd, dd MMM yyyy HH:mm:ss GMT",
                                                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out pubDate))
                    {
                        Console.WriteLine($"Failed to parse date: {pubDateString}");
                    }
                }

                NewsModel rssItem = new NewsModel
                {
                    Title = item.Element("title")?.Value,
                    Description = item.Element("description")?.Value,
                    Link = item.Element("link")?.Value,
                    PubDate = pubDate
                };
                items.Add(rssItem);
            }

            return items;
        }
    }
}