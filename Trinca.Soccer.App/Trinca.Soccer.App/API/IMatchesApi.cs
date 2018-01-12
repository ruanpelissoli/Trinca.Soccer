using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.API
{
    [Headers("Accept: application/json")]
    public interface IMatchesApi
    {
        [Get("/matches")]
        Task<IEnumerable<MatchModel>> GetAll();

        [Get("/matches/{id}")]
        Task<MatchModel> GetById(int id);
    }
}
