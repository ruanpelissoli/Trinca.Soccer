using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Models;
using Trinca.Soccer.Services.Mapping;

namespace Trinca.Soccer.Services
{
    public interface IMatchesService
    {
        Task<IEnumerable<MatchListOutputDto>> GetAll();
        Task<MatchOutputDto> GetById(int matchId);
        Task<MatchOutputDto> Create(MatchInputDto match);
        Task Update(MatchInputDto match);
        Task Delete(MatchInputDto match);        
    }

    public class MatchesService : IMatchesService
    {
        private readonly IMatchesRepository _matchesRepository;
        private readonly IPlayersService _playersServices;

        public MatchesService(IMatchesRepository matchesRepository, IPlayersService playersServices)
        {
            _matchesRepository = matchesRepository;
            _playersServices = playersServices;
        }

        public async Task<IEnumerable<MatchListOutputDto>> GetAll()
        {
            var matches = await _matchesRepository.GetAllAsync();

            var matchesListOutputDto = MappingConfig.Mapper().Map<List<MatchListOutputDto>>(matches);

            foreach (var matchListOutputDto in matchesListOutputDto)
            {
                var totalPlayers = await _playersServices.GetAllByMatch(matchListOutputDto.Id);
                matchListOutputDto.TotalPlayers = $"{totalPlayers.Count()}/{matchListOutputDto.MinimumPlayers}";
            }

            return matchesListOutputDto.OrderByDescending(o => o.CreateDate);
        }

        public async Task<MatchOutputDto> GetById(int matchId)
        {
            var match = await _matchesRepository.FindAsync(matchId);
            var matchOutput = MappingConfig.Mapper().Map<MatchOutputDto>(match);

            if (matchOutput == null) return new MatchOutputDto();
            
            return matchOutput;
        }        

        public async Task<MatchOutputDto> Create(MatchInputDto matchInput)
        {
            var match = MappingConfig.Mapper().Map<Match>(matchInput);
            match = await _matchesRepository.CreateAsync(match);

            return MappingConfig.Mapper().Map<MatchOutputDto>(match);
        }

        public async Task Update(MatchInputDto matchInput)
        {
            var match = MappingConfig.Mapper().Map<Match>(matchInput);
            await _matchesRepository.UpdateAsync(match);
        }

        public async Task Delete(MatchInputDto matchInput)
        {
            var match = MappingConfig.Mapper().Map<Match>(matchInput);
            await _matchesRepository.DeleteAsync(match);
        }
    }
}
