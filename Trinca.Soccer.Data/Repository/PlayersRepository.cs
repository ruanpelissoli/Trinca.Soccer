using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Data.Repository
{
    public class PlayersRepository : BaseRepository<Player>, IPlayersRepository
    {
        public PlayersRepository(DatabaseContext.IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
