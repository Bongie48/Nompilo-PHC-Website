using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Nompilo_PHC_Website.Models
{
    public class ContraceptiveRecordN
    {
        [Key]
        public int CRId { get; set; }
        [StringLength(13)]
        [Required(ErrorMessage = "Please provide patient ID number")]
        [DisplayName("Identity Number")]
        public string IdentityNumber { get; set; }
        [Required]
        [DisplayName("QSy")]
        public string QSym { get; set; }
        [Required(ErrorMessage = "Please enter patient symptoms or write 'N/A' if not applicable")]
        [DisplayName("Symptoms ")]
        public string Symptoms { get; set; }
        [Required(ErrorMessage = "Please select date")]
        [DisplayName("Consultation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CurrentDate { get; set; }


        [Required(ErrorMessage = "Please enter patient blood pressure")]
        [DisplayName("Blood Pressure (Bp)")]
        public string BloodPressure { get; set; }


        [Required(ErrorMessage = "Please enter patient weight")]
        [DisplayName("Weight (Wt) ")]
        public float Weight { get; set; }


        [Required(ErrorMessage = "Please select select the date")]
        [DisplayName("Last Menstrual Period")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastMenstrualPeriod { get; set; }


        [Required(ErrorMessage = "Please select from the options provided")]
        public string Method { get; set; }
        [Required]
        [DisplayName("Pregnancy Test Results")]
        public string TestResults { get; set; }
        [Required(ErrorMessage = "Please select the date")]
        [DisplayName("Return Date (TCA)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddres { get; set; }
        public string Year { get; set; }
        public string MonthYear { get; set; }
        
    }
}
