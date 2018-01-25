using System.Collections.Generic;
using Trinca.Soccer.Dto.Player;

namespace Trinca.Soccer.Dto.Match
{
    public class MatchListOutputDto
    {
        public int Id { get; set; }
        public string Place { get; set; }
        public string CreatedBy { get; set; }
        public int MinimumPlayers { get; set; }
        public string Score { get; set; }
        public string TotalPlayers { get; set; }
    }
}
