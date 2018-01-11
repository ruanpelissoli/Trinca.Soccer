using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.API.Models;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.API.Controllers
{
    [RoutePrefix("api/employees")]
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

        [Route("login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginViewModel loginViewModel)
        {
            var token = await _employeesServices.Login(loginViewModel.Username, loginViewModel.Password);

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }
    }
}
