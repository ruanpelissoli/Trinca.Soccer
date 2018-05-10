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

        [HttpGet]
        [Route("password/check/{id}/{currentPassword}")]
        public async Task<IHttpActionResult> CheckPassword(int id, string currentPassword)
        {
            return await TryCatchAsync(async () =>
            {
                var passworkOk = await _employeesService.CheckPassword(id, currentPassword);
                return Ok(passworkOk);
            });
        }

        [HttpPut]
        [Route("password/change/{id}/{newPassword}")]
        public async Task<IHttpActionResult> ChangePassword(int id, string newPassword)
        {
            return await TryCatchAsync(async () =>
            {
                await _employeesService.ChangePassword(id, newPassword);
                return Ok();
            });
        }
    }
}
