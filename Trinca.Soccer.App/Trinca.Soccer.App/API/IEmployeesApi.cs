using System.Threading.Tasks;
using Refit;
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.API
{
    [Headers("Accept: application/json")]
    public interface IEmployeesApi
    {
        [Post("/employees/login")]
        Task<EmployeeModel> Login(LoginModel model);
    }
}
