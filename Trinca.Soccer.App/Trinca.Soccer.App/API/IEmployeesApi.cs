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

        [Get("/employees/password/check/{id}/{currentPassword}")]
        Task<bool> CheckPassword(int id, string currentPassword);

        [Put("/employees/password/change/{id}/{newPassword}")]
        Task ChangePassword(int id, string newPassword);
    }
}
