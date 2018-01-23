using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trinca.Soccer.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public int MinimumPlayers { get; set; }
        public decimal Value { get; set; }
        public bool WithBarbecue { get; set; }
        public decimal BarbecueValue { get; set; }
        public bool IsFinished { get; set; }
        public int BlueTeamScore { get; set; }
        public int RedTeamScore { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual Employee Employee { get; set; }
        public virtual List<Player> Players { get; set; }
    }
}