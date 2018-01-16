using System;
using System.Collections.Generic;

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
    }
}
