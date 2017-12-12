using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Data.Repository
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(DatabaseContext.IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
