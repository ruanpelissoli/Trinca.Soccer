using System.Threading.Tasks;
using Refit;
using Trinca.Soccer.Dto.Employee;
using Trinca.Soccer.Dto.Login;

namespace Trinca.Soccer.App.API
{
    [Headers("Accept: application/json")]
    public interface IEmployeesApi
    {
        [Post("/employees/login")]
        Task<LoginOutputDto> Login(LoginInputDto model);

        [Get("/employees/{id}")]
        Task<EmployeeOutputDto> GetEmployee(int id);
    }
}
