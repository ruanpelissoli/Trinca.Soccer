using System;
using System.Linq;
using System.Threading.Tasks;
using Trinca.Soccer.App.API;

namespace Trinca.Soccer.App.Models
{
    public class MatchModel
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public int MinimumPlayers { get; set; }
        public decimal Value { get; set; }
        public bool WithBarbecue { get; set; }
        public decimal BarbecueValue { get; set; }
        public bool IsFinished { get; set; }
        public int BlueTeamScore { get; set; }
        public int RedTeamScore { get; set; }
        public DateTime CreateDate { get; set; }
        public EmployeeModel Employee { get; set; }

        public string PlaceTime { get { return $"{Place} - {Date.ToString("dd/MM/yyyy hh:mm:ss")}"; } }
        public string CreatedByTitle { get { return Employee != null ? $"Created by: {Employee.Name}" : string.Empty; } }
        public string Score { get { return IsFinished ? $"{BlueTeamScore}x{RedTeamScore}" : string.Empty; } }
        public async Task<string> TotalPlayers()
        {
            var players = await ClientApi.Players.GetAllByMatch(Id);
            return $"Players: {players.Count()}/{MinimumPlayers}";
        }
    }
}
