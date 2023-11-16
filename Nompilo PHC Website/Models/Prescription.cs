
using Nompilo_PHC_Website.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        public string Medication { get; set; }

        public string Dosage { get; set; }

        public string Instructions { get; set; }

        public int CheckUpId { get; set; }

        [ForeignKey("CheckUpId")]
        public Checkup Checkup { get; set; }

        public string? DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DataGeeksUser? DataGeeksUser { get; set; }
    }
}
