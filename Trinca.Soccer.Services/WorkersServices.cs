using System.Collections.Generic;
using System.Threading.Tasks;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Services
{
    public interface IWorkersServices
    {
        Task<IEnumerable<Worker>> GetAll();
    }

    public class WorkersServices : IWorkersServices
    {
        private readonly IWebScraper _webScraper;

        public WorkersServices(IWebScraper webScraper)
        {
            _webScraper = webScraper;
        }

        public async Task<IEnumerable<Worker>> GetAll()
        {
            return await _webScraper.GetTrincaWorkers(@"http://trin.ca");
        }
    }
}
