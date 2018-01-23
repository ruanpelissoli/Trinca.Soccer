using System.Threading.Tasks;
using System.Web.Http;
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
                return Ok(await _playersServices.GetAllByMatch(matchId));
            });
        }

        [Route("{matchId}/{teamId}")]
        public async Task<IHttpActionResult> GetByMatch(int matchId, ETeams teamId)
        {
            return await TryCatchAsync(async () =>
            {
                return Ok(await _playersServices.GetAllByTeam(matchId, teamId));
            });
        }
    }
}
