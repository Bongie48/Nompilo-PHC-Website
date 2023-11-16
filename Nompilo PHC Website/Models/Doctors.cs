using System.ComponentModel.DataAnnotations;

namespace Nompilo_PHC_Website.Models
{
    public class Doctors
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string DoctorSurname { get; set; }
        [Required]
        public string Qualifications { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Tittle { get; set; }
        [Required]
        public string Speciality { get; set; }
    }
}
