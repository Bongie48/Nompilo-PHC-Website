
using Nompilo_PHC_Website.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class Checkup
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Check-Up Date")]
        public DateTime CheckupDate { get; set; }

        public string? Results { get; set; }

        [Display(Name = "Condition")]

        public string Notes { get; set; }

        [Required]
        [Display(Name = "Weight (in kg)")]
        public decimal? Weight { get; set; }

        [Display(Name = "Blood Pressure")]
        public string? BloodPressure { get; set; }

        [Range(1, 9, ErrorMessage = "Prenatal month must be between 1 and 9.")]
        public int? PrenatalMonth { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Temperature must be between 0 and 100.")]
        public decimal? Temperature { get; set; }

        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }

        public string DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public DataGeeksUser DataGeeksUser { get; set; }


        public string PatientId { get; set; }

        [ForeignKey("PatientId")]
        public DataGeeksUser DataGeeksPatient { get; set; }


    }


}
