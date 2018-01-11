using System.Threading.Tasks;
using Refit;
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.ApiClient
{
    [Headers("Accept: application/json")]
    public interface IEmployeesApi
    {
        [Post("/employees/login")]
        Task<string> Login(LoginModel model);
    }
}
