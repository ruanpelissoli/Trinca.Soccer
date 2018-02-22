using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.API.Controllers
{
    [RoutePrefix("matches")]
    public class MatchesController : BaseController
    {
        private readonly IMatchesService _matchesServices;
        private readonly IPlayersService _playersServices;

        public MatchesController(IMatchesService matchesServices, IPlayersService playersServices)
        {
            _matchesServices = matchesServices;
            _playersServices = playersServices;
        }

        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            return await TryCatchAsync(async () =>
            {
                var matches = await _matchesServices.GetAll();
                return Ok(matches);
            });            
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            return await TryCatchAsync(async () =>
            {
                var match = await _matchesServices.GetById(id);

                if (match.Id == 0)
                    return NotFound();

                return Ok(match);
            });            
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create(MatchInputDto matchInput)
        {
            return await TryCatchAsync(async () =>
            {
                var match = await _matchesServices.Create(matchInput);
                return Ok(match);
            });            
        }
    }
}
