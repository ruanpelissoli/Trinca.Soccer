using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinca.Soccer.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Place { get; set; }
        public string Time { get; set; }
        public int MinimumPlayers { get; set; }
        public decimal Value { get; set; }
        public bool WithBarbecue { get; set; }
        public decimal BarbecueValue { get; set; }
        public bool IsFinished { get; set; }
        public int BlueTeamScore { get; set; }
        public int RedTeamScore { get; set; }

        public virtual List<Team> Teams { get; set; }
    }
}