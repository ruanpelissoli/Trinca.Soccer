using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.API.Controllers
{
    public class EmployeesController : ApiController
    {
        private readonly IEmployeesServices _employeesServices;

        public EmployeesController(IEmployeesServices employeesServices)
        {
            _employeesServices = employeesServices;
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var employees = await _employeesServices.GetAll();
            return Ok(employees);
        }

        public async Task<IHttpActionResult> CreateAllFromSite()
        {
            await _employeesServices.CreateAllFromSite();
            return Ok();
        }

        public async Task<IHttpActionResult> GetAllFromSite()
        {
            var employees = await _employeesServices.GetAllFromSite();
            return Ok(employees);
        }
    }
}
