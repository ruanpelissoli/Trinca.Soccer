using System.Linq;
using System.Threading.Tasks;
using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Data.Repository
{
    public class EmployeesRepository : BaseRepository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(DatabaseContext.IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

        public async Task<Employee> GetLoggedUser(string userName, string password)
        {
            var employees = await GetAllAsync(w => w.Username == userName && w.Password == password);
            return employees.FirstOrDefault();
        }
    }
}
