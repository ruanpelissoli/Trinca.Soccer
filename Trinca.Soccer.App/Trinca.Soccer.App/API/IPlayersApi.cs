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
        [Get("/players/bymatch/{matchId}")]
        Task<IEnumerable<PlayerOutputDto>> GetAllByMatch(int matchId);

        [Get("/players/bymatch/{matchId}/{teamId}")]
        Task<IEnumerable<PlayerOutputDto>> GetAllByTeam(int matchId, ETeams teamId);

        [Post("/players")]
        Task<PlayerOutputDto> Create(PlayerInputDto playerInput);

        [Put("/players")]
        Task Update(PlayerOutputDto playerInput);

        [Delete("/players/{id}")]
        Task Delete(int id);

        [Get("/players/{id}")]
        Task<PlayerOutputDto> GetById(int id);
    }
}
