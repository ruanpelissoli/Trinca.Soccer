using System.ComponentModel.DataAnnotations;

namespace Trinca.Soccer.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PictureUrl { get; set; }
        public string Password { get; set; }
    }
}
