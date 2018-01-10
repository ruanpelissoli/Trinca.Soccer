using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Data.Repository
{
    public class EmployeesRepository : BaseRepository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(DatabaseContext.IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
