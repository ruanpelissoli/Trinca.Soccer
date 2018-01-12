using System.Collections.Generic;
using System.Threading.Tasks;
using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;

namespace Trinca.Soccer.Services
{
    public interface IMatchesService
    {
        Task<IEnumerable<Match>> GetAll();
        Task<Match> GetById(int matchId);
        Task<Match> Create(Match match);
        Task Update(Match match);
        Task Delete(Match match);
    }

    public class MatchesService : IMatchesService
    {
        private readonly IMatchesRepository _matchesRepository;

        public MatchesService(IMatchesRepository matchesRepository)
        {
            _matchesRepository = matchesRepository;
        }

        public async Task<IEnumerable<Match>> GetAll()
        {
            return await _matchesRepository.GetAllAsync();
        }

        public async Task<Match> GetById(int matchId)
        {
            var match = await _matchesRepository.FindAsync(matchId);

            return match ?? new Match();
        }

        public async Task<Match> Create(Match match)
        {
            return await _matchesRepository.CreateAsync(match);
        }

        public async Task Update(Match match)
        {
            await _matchesRepository.UpdateAsync(match);
        }

        public async Task Delete(Match match)
        {
            await _matchesRepository.DeleteAsync(match);
        }
    }
}
