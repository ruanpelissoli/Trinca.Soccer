using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Trinca.Soccer.Dto.Match;

namespace Trinca.Soccer.App.API
{
    [Headers("Accept: application/json")]
    public interface IMatchesApi
    {
        [Get("/matches")]
        Task<IEnumerable<MatchListOutputDto>> GetAll();

        [Get("/matches/{id}")]
        Task<MatchOutputDto> GetById(int id);

        [Post("/matches")]
        Task<MatchOutputDto> Create(MatchInputDto match);
    }
}
