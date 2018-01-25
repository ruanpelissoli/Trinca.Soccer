using Trinca.Soccer.Dto.Employee;
using Trinca.Soccer.Dto.Enums;
using Trinca.Soccer.Dto.Match;

namespace Trinca.Soccer.Dto.Player
{
    public class PlayerOutputDto
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public ETeams TeamId { get; set; }
        public string Name { get; set; }
        public int? EmployeeId { get; set; }
        public bool IsGuest { get; set; }

        //public MatchOutputDto Match { get; set; }
        //public EmployeeOutputDto Employee { get; set; }
    }
}
