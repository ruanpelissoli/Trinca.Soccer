using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.Dto.Player;
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

        [Route("bymatch/{matchId}")]
        public async Task<IHttpActionResult> GetByMatch(int matchId)
        {
            return await TryCatchAsync(async () =>
            {
                var players = await _playersServices.GetAllByMatch(matchId);
                return Ok(players);
            });
        }

        [Route("bymatch/{matchId}/{teamId}")]
        public async Task<IHttpActionResult> GetByTeam(int matchId, ETeams teamId)
        {
            return await TryCatchAsync(async () =>
            {
                var players = await _playersServices.GetAllByTeam(matchId, teamId);
                return Ok(players);
            });
        }

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Create(PlayerInputDto playerDto)
        {
            return await TryCatchAsync(async () =>
            {
                var player = await _playersServices.Create(playerDto);
                return Ok(player);
            });
        }

        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(PlayerInputDto playerDto)
        {
            return await TryCatchAsync(async () =>
            {
                await _playersServices.Update(playerDto);
                return Ok();
            });
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            return await TryCatchAsync(async () =>
            {
                await _playersServices.Delete(id);

                return Ok();
            });
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            return await TryCatchAsync(async () =>
            {
                var player = await _playersServices.GetById(id);
                return Ok(player);
            });
        }

        [Route("{matchId}/{id}")]
        public async Task<IHttpActionResult> GetByEmployeeId(int id, int matchId)
        {
            return await TryCatchAsync(async () =>
            {
                var player = await _playersServices.GetByEmployeeId(id, matchId);
                return Ok(player);
            });
        }
    }
}
