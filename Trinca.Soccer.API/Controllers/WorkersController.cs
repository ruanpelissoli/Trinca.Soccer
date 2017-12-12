using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.Models;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.API.Controllers
{
    public class WorkersController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            var workersService = new WorkersServices(new WebScraper());
            var workers = await workersService.GetAll();

            return Ok(workers);
        }
    }
}
