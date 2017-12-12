using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Data.Repository
{
    public class WorkerRepository : BaseRepository<Worker>, IWorkerRepository
    {
        public WorkerRepository(DatabaseContext.IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
