using Trinca.Soccer.Dto.Enums;

namespace Trinca.Soccer.Dto.Player
{
    public class PlayerInputDto
    {
        public int MatchId { get; set; }
        public ETeams TeamId { get; set; }
        public string Name { get; set; }
        public int? EmployeeId { get; set; }
        public bool IsGuest { get; set; }
    }
}
