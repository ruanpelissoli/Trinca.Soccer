﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trinca.Soccer.Models.Enums;

namespace Trinca.Soccer.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public int MatchId { get; set; }
        public ETeams TeamId { get; set; }
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public bool IsGuest { get; set; }
        public bool WithBarbecue { get; set; }

        [ForeignKey(nameof(MatchId))]
        public virtual Match Match { get; set; }
    
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }
    }
}
