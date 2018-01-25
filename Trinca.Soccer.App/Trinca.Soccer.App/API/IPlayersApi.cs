using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trinca.Soccer.App.Util;
using Trinca.Soccer.Dto.Player;

namespace Trinca.Soccer.App.API
{
    [Headers("Accept: application/json")]
    public interface IPlayersApi
    {
        [Get("/players/{matchId}")]
        Task<IEnumerable<PlayerOutputDto>> GetAllByMatch(int matchId);
        [Get("/players/{matchId}/{teamId}")]
        Task<IEnumerable<PlayerOutputDto>> GetAllByTeam(int matchId, ETeams teamId);
        [Post("/players")]
        Task<PlayerOutputDto> Create(PlayerInputDto playerInput);
    }
}
