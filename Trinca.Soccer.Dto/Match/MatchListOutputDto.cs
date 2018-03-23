using System;

namespace Trinca.Soccer.Dto.Match
{
    public class MatchListOutputDto
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public string Date { get; set; }
        public string CreatedByPictureUrl { get; set; }
        public int MinimumPlayers { get; set; }
        public string Score { get; set; }
        public string TotalPlayers { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
