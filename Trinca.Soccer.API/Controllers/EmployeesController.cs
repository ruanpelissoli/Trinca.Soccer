using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.API.Models;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.API.Controllers
{
    [RoutePrefix("employees")]
    public class EmployeesController : BaseController
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IHttpActionResult> Login(LoginViewModel loginViewModel)
        {
            return await TryCatchAsync(async () =>
            {
                var employee = await _employeesService.Login(loginViewModel.Username, loginViewModel.Password);

                if (employee.Id == 0)
                    return Unauthorized();

                return Ok(employee);
            });
        }
    }
}
