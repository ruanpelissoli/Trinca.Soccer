using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Services
{
    public interface IWebScraper
    {
        Task<IEnumerable<Employee>> GetTrincaWorkers(string url);
    }

    public class WebScraper : IWebScraper
    {
        private async Task<HtmlDocument> GetHtmlDocument(string url)
        {
            var document = new HtmlDocument();

            try
            {
                var client = new HttpClient();
                var data = await client.GetStringAsync(url);

                document.LoadHtml(data);

                return document;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return document;
        }

        public async Task<IEnumerable<Employee>> GetTrincaWorkers(string url)
        {
            var htmlDocument = await GetHtmlDocument(url);

            var htmlWorkersList = htmlDocument.DocumentNode.Descendants("div")
                .Where(w =>
                    w.Attributes.Contains("class")
                    &&
                    w.Attributes["class"].Value.Contains("the-thumb"));

            return (from item in htmlWorkersList
                let workerName = item.Descendants("p").FirstOrDefault()?.InnerText.Trim()
                let workerUserName = GetUsername(item.Descendants("img").FirstOrDefault()?.Attributes["src"].Value)
                let workerPicture = item.Descendants("img").FirstOrDefault()?.Attributes["src"].Value
                where workerPicture == null || !workerPicture.Contains("joker")
                select new Employee
                {
                    Name = workerName,
                    Username = workerUserName,
                    PictureUrl = workerPicture
                }).ToList();
        }

        private string GetUsername(string pictureUrl)
        {
            var end = pictureUrl.IndexOf(".jpg");
            return pictureUrl.Substring(0, end - 0).Replace("http://trin.ca/wp-content/themes/trinca/interface/build/img/team/", "").ToLower().Replace("-", ".");
        }
    }
}
