using System.Collections.Generic;
using System.Threading.Tasks;
using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Models;
using Trinca.Soccer.Models.Enums;

namespace Trinca.Soccer.Services
{
    public interface IPlayersService
    {
        Task<IEnumerable<Player>> GetAllByMatch(int matchId);
        Task<IEnumerable<Player>> GetAllByTeam(int matchId, ETeams teamId);
    }

    public class PlayersService : IPlayersService
    {
        private readonly IPlayersRepository _playersRepository;

        public PlayersService(IPlayersRepository playersRepository)
        {
            _playersRepository = playersRepository;
        }

        public async Task<IEnumerable<Player>> GetAllByMatch(int matchId)
        {
            return await _playersRepository.GetAllAsync(w => w.MatchId == matchId);
        }

        public async Task<IEnumerable<Player>> GetAllByTeam(int matchId, ETeams teamId)
        {
            return await _playersRepository.GetAllAsync(w => w.MatchId == matchId && w.TeamId == teamId);
        }
    }
}
