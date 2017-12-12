using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trinca.Soccer.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public int MinimumPlayers { get; set; }
        public int Players { get; set; }
        public string Place { get; set; }
        public bool Canceled { get; set; }
        public bool Finished { get; set; }

        public virtual List<Worker> Workers { get; set; }
    }
}