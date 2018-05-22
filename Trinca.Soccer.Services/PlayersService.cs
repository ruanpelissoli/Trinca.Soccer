using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinca.Soccer.Data.Interfaces;
using Trinca.Soccer.Dto.Player;
using Trinca.Soccer.Models;
using Trinca.Soccer.Models.Enums;
using Trinca.Soccer.Services.Mapping;

namespace Trinca.Soccer.Services
{
    public interface IPlayersService
    {
        Task<IEnumerable<PlayerOutputDto>> GetAllByMatch(int matchId);
        Task<IEnumerable<PlayerOutputDto>> GetAllByTeam(int matchId, ETeams teamId);
        Task<PlayerOutputDto> Create(PlayerInputDto playerInput);
        Task Update(PlayerInputDto playerInput);
        Task Delete(int id);
        Task<PlayerOutputDto> GetById(int id);
        Task<PlayerOutputDto> GetByEmployeeId(int id, int matchId);
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

        public async Task<IEnumerable<PlayerOutputDto>> GetAllByMatch(int matchId)
        {
            var players = await _playersRepository.GetAllAsync(w => w.MatchId == matchId);

            return MappingConfig.Mapper().Map<List<PlayerOutputDto>>(players);
        }

        public async Task<IEnumerable<PlayerOutputDto>> GetAllByTeam(int matchId, ETeams teamId)
        {
            var players = await _playersRepository.GetAllAsync(w => w.MatchId == matchId && w.TeamId == teamId);

            return MappingConfig.Mapper().Map<List<PlayerOutputDto>>(players);
        }

        public async Task<PlayerOutputDto> Create(PlayerInputDto playerInput)
        {
            var player = MappingConfig.Mapper().Map<Player>(playerInput);
            player = await _playersRepository.CreateAsync(player);

            if (player.Employee == null)
                player.Employee = await _employeesRepository.FindAsync(player.EmployeeId);

            return MappingConfig.Mapper().Map<PlayerOutputDto>(player);
        }

        public async Task Update(PlayerInputDto playerInput)
        {
            var player = MappingConfig.Mapper().Map<Player>(playerInput);
            await _playersRepository.UpdateAsync(player);
        }

        public async Task Delete(int id)
        {
            await _playersRepository.DeleteAsync(w => w.Id == id);
        }

        public async Task<PlayerOutputDto> GetById(int id)
        {
            var player = await _playersRepository.FindAsync(id);
            return MappingConfig.Mapper().Map<PlayerOutputDto>(player);
        }

        public async Task<PlayerOutputDto> GetByEmployeeId(int id, int matchId)
        {
            var employees = await _playersRepository.GetAllAsync(w => w.EmployeeId == id && w.MatchId == matchId && !w.IsGuest);
            var employee = employees.FirstOrDefault();

            return MappingConfig.Mapper().Map<PlayerOutputDto>(employee);
        }
    }
}
