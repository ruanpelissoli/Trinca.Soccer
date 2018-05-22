using System;
using System.Collections.Generic;
using Trinca.Soccer.Dto.Employee;
using Trinca.Soccer.Dto.Player;

namespace Trinca.Soccer.Dto.Match
{
    public class MatchOutputDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public string Place { get; set; }
        public string Date { get; set; }
        public int MinimumPlayers { get; set; }
        public decimal Value { get; set; }
        public bool WithBarbecue { get; set; }
        public decimal BarbecueValue { get; set; }
        public bool IsFinished { get; set; }
        public int YellowTeamScore { get; set; }
        public int BlackTeamScore { get; set; }
        public DateTime CreateDate { get; set; }

        public EmployeeOutputDto Employee { get; set; }
        public List<PlayerOutputDto> Players { get; set; }
    }
}
