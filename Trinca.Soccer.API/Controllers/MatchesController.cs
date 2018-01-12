using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Trinca.Soccer.API.Models;
using Trinca.Soccer.Services;

namespace Trinca.Soccer.API.Controllers
{
    [RoutePrefix("matches")]
    public class MatchesController : ApiController
    {
        private readonly IMatchesService _matchesServices;

        public MatchesController(IMatchesService matchesServices)
        {
            _matchesServices = matchesServices;
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var matches = await _matchesServices.GetAll();
            return Ok(matches);
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var match = await _matchesServices.GetById(id);

            if (match.Id == 0)
                return NotFound();

            return Ok(match);
        }
    }
}
