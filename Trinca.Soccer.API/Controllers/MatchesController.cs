using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.API.Models;
using Trinca.Soccer.Models;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.API.Controllers
{
    [RoutePrefix("matches")]
    public class MatchesController : BaseController
    {
        private readonly IMatchesService _matchesServices;

        public MatchesController(IMatchesService matchesServices)
        {
            _matchesServices = matchesServices;
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
        public async Task<IHttpActionResult> Create(Match match)
        {
            return await TryCatchAsync(async () =>
            {
                return Ok(await _matchesServices.Create(match));
            });            
        }
    }
}
