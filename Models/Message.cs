using System.ComponentModel.DataAnnotations;

namespace MercuryMVC.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public string MessageText { get; set; }
        public DateTime TimeSent { get; set; }

        public AppUser AppUser { get; set; }
    }
}
