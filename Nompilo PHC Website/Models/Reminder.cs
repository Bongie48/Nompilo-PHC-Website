using System.ComponentModel.DataAnnotations;

namespace Nompilo_PHC_Website.Models
{
    public class Reminder
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Days { get; set; }
        [Required]
        public string Time { get; set; }
        public string email { get; set; }
    }
}
