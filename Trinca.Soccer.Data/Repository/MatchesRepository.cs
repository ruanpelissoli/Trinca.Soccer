using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Data.Repository
{
    public class MatchesRepository : BaseRepository<Match>, IMatchesRepository
    {
        public MatchesRepository(DatabaseContext.IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }
    }
}
