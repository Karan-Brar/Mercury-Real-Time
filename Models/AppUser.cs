using System.ComponentModel.DataAnnotations;

namespace MercuryMVC.Models
{
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public List<Message> Messages { get; } = new();
    }
}
