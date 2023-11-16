using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Nompilo_PHC_Website.Models
{
    public class PrenatalPatients  
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Reference Date")]
        public DateTime Date { get; set; }
        [Required]
        [DisplayName("Months Pregnant")]
        public int Months { get; set; }

        [Required]
        [DisplayName("Weight")]
        public int Weight { get; set; }

        [Required]
        [DisplayName("Urine Test")]
        public string Urine { get; set; }
    }
}
