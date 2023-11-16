using Nompilo_PHC_Website.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class Test
    {
        [Key]
        public int testId { get; set; }
        [Required]
        public string email { get; set; } //Email is for the doctor that sends the samples
        [Required]
        public string pName { get; set; }
        [Required]
        public string pSurname { get; set; }
        [Required]
        public string testfor { get; set; }
        public string testStatus { get; set; }

        public string? PatientId { get; set; }

        [ForeignKey("PatientId")]
        public DataGeeksUser? Patients { get; set; }

        public string? DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DataGeeksUser? Doctors { get; set; }

        public DateTime DateCreated { get; set; }

        public string PatientName
        {
            get
            {
                return $"{pName} {pSurname}";
            }
        }


        public ICollection<TestMethod> TestMethods { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
        public ICollection<Instrument> Instruments { get; set; }
    }
}
