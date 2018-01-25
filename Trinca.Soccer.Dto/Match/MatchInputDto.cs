using System;

namespace Trinca.Soccer.Dto.Match
{
    public class MatchInputDto
    {
        public int CreatedBy { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public int MinimumPlayers { get; set; }
        public decimal Value { get; set; }
        public bool WithBarbecue { get; set; }
        public decimal BarbecueValue { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
