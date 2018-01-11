using System.Threading.Tasks;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Data.Interfaces
{
    public interface IEmployeesRepository : IBaseRepository<Employee>
    {
        Task<Employee> GetLoggedUser(string userName, string password);
    }
}
