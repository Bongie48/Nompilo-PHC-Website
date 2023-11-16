using System.ComponentModel.DataAnnotations;

namespace Nompilo_PHC_Website.Models
{
    public class Chronicpatients
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Chronicdisease { get; set; }
    }
}
