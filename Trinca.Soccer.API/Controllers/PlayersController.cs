using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.API.Mapping;
using Trinca.Soccer.Dto.Player;
using Trinca.Soccer.Models;
using Trinca.Soccer.Models.Enums;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.API.Controllers
{
    [RoutePrefix("players")]
    public class PlayersController : BaseController
    {
        private readonly IPlayersService _playersServices;

        public PlayersController(IPlayersService playersServices)
        {
            _playersServices = playersServices;
        }

        [Route("{matchId}")]
        public async Task<IHttpActionResult> GetByMatch(int matchId)
        {
            return await TryCatchAsync(async () =>
            {
                var players = await _playersServices.GetAllByMatch(matchId);

                return Ok(MappingConfig.Mapper().Map<List<PlayerOutputDto>>(players));
            });
        }

        [Route("{matchId}/{teamId}")]
        public async Task<IHttpActionResult> GetByMatch(int matchId, ETeams teamId)
        {
            return await TryCatchAsync(async () =>
            {
                var players = await _playersServices.GetAllByTeam(matchId, teamId);

                return Ok(MappingConfig.Mapper().Map<List<PlayerOutputDto>>(players));
            });
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(PlayerInputDto playerDto)
        {
            return await TryCatchAsync(async () =>
            {
                var player = MappingConfig.Mapper().Map<Player>(playerDto);
                player = await _playersServices.Create(player);

                return Ok(MappingConfig.Mapper().Map<PlayerOutputDto>(player));
            });
        }
    }
}
