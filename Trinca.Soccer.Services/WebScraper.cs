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
                let workerPicture = item.Descendants("img").FirstOrDefault()?.Attributes["src"].Value
                where workerPicture == null || !workerPicture.Contains("joker")
                select new Employee
                {
                    Name = workerName,
                    PictureUrl = workerPicture
                }).ToList();
        }
    }
}
