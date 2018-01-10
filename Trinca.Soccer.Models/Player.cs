using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trinca.Soccer.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? EmployeeId { get; set; }
        public bool IsGuest { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
    }
}
