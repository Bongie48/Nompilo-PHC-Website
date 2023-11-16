using System.ComponentModel.DataAnnotations;

namespace Nompilo_PHC_Website.Models
{
    public class Rvalues
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int value { get; set; }

    }
}