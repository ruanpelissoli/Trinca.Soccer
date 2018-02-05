using System.Collections.Generic;
using System.Linq;
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
        Task<Player> Create(Player player);
        Task Update(Player player);
        Task Delete(int id);
        Task<Player> GetById(int id);
        Task<Player> GetByEmployeeId(int id);
    }

    public class PlayersService : IPlayersService
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IEmployeesRepository _employeesRepository;

        public PlayersService(IPlayersRepository playersRepository, IEmployeesRepository employeesRepository)
        {
            _playersRepository = playersRepository;
            _employeesRepository = employeesRepository;
        }

        public async Task<IEnumerable<Player>> GetAllByMatch(int matchId)
        {
            return await _playersRepository.GetAllAsync(w => w.MatchId == matchId);
        }

        public async Task<IEnumerable<Player>> GetAllByTeam(int matchId, ETeams teamId)
        {
            return await _playersRepository.GetAllAsync(w => w.MatchId == matchId && w.TeamId == teamId);
        }

        public async Task<Player> Create(Player player)
        {
            player = await _playersRepository.CreateAsync(player);

            if (player.Employee == null)
                player.Employee = await _employeesRepository.FindAsync(player.EmployeeId);

            return player;
        }

        public async Task Update(Player player)
        {
            await _playersRepository.UpdateAsync(player);
        }

        public async Task Delete(int id)
        {
            await _playersRepository.DeleteAsync(w => w.Id == id);
        }

        public async Task<Player> GetById(int id)
        {
           return await _playersRepository.FindAsync(id);
        }

        public async Task<Player> GetByEmployeeId(int id)
        {
            var employees = await _playersRepository.GetAllAsync(w => w.EmployeeId == id && !w.IsGuest);
            return employees.FirstOrDefault();
        }
    }
}
