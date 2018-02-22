using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.Dto.Login;
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
        public async Task<IHttpActionResult> Login(LoginInputDto loginDto)
        {
            return await TryCatchAsync(async () =>
            {
                var employee = await _employeesService.Login(loginDto.Username, loginDto.Password);

                if (employee.Id == 0)
                    return Unauthorized();

                return Ok(employee);
            });
        }
        [Route("{id}")]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            return await TryCatchAsync(async () =>
            {
                var employee = await _employeesService.GetById(id);
                return Ok(employee);
            });
        }
    }
}
