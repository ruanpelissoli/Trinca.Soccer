using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trinca.Soccer.App.Models;
using Trinca.Soccer.App.Util;

namespace Trinca.Soccer.App.API
{
    [Headers("Accept: application/json")]
    public interface IPlayersApi
    {
        [Get("/players/{matchId}")]
        Task<IEnumerable<PlayersModel>> GetAllByMatch(int matchId);
        [Get("/players/{matchId}/{teamId}")]
        Task<IEnumerable<PlayersModel>> GetAllByTeam(int matchId, ETeams teamId);
    }
}
