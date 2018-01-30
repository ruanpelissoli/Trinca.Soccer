using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.API.Mapping;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Models;
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

                var matchesListOutputDto = MappingConfig.Mapper().Map<List<MatchListOutputDto>>(matches);

                foreach (var matchListOutputDto in matchesListOutputDto)
                {
                    var totalPlayers = await _playersServices.GetAllByMatch(matchListOutputDto.Id);
                    matchListOutputDto.TotalPlayers = $"{totalPlayers.Count()}/{matchListOutputDto.MinimumPlayers} Players";
                }

                return Ok(matchesListOutputDto.OrderByDescending(o => o.CreateDate));
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

                return Ok(MappingConfig.Mapper().Map<MatchOutputDto>(match));
            });            
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create(MatchInputDto matchInput)
        {
            return await TryCatchAsync(async () =>
            {
                var match = MappingConfig.Mapper().Map<Match>(matchInput);
                match = await _matchesServices.Create(match);

                return Ok(MappingConfig.Mapper().Map<MatchOutputDto>(match));
            });            
        }
    }
}
