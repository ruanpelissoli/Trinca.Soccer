using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trinca.Soccer.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public int GameId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }

        [ForeignKey(nameof(GameId))]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public virtual Player Player { get; set; }
    }
}