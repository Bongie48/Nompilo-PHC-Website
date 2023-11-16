using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nompilo_PHC_Website.Models
{
    public class EmeContra
    {
        [Key] 
        public int EmeId { get; set; }
        [StringLength(13)]
        [Required(ErrorMessage = "Please provide patient ID number")]
        [DisplayName("Identity Number")]
        public string IdentityNumber { get; set; }
        [Required(ErrorMessage = "Please provide patient email address")]
        [DisplayName("Email Address")]
        public string EmailAddres { get; set; }
        public string Year { get; set; }
        public string QSym { get; set; }
        
        [Required(ErrorMessage = "Please provide consultation date")]
        [DisplayName(" Consultation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CurrentDate { get; set; }
        [Required(ErrorMessage = "Please enter your name as a nurse")]
        [DisplayName("Enter your full name (Nurse's Name) ")]
        public string NurseName { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Length of time since unprotected sex occured? ")]
        public string SexLength { get; set;}
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Was the last period normal? ")]
        public string PeriodNorm { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Is this the first occassion of unprotect sex since last period?  ")]
        public string SexOcc { get; set; }
        [Required]
        [DisplayName("Has the patient used Emergency Hormonal Contraceptive before? ")]
        public string EHCBefore { get; set; }
        [Required]
        [DisplayName("Has patient used Emergency Hormonal Contraceptive since last period? ")]
        public string EHCLastPeriod { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("If Yes any adversely reaction? (Please provide details below")]
        public string EHCSym { get; set; }
        [Required(ErrorMessage = "Please provide the symptom or write 'N/A' if not applicable")]
        public string EHCSymDe { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Does a patient have any allegies? ")]
        public string Alleg { get; set; }
        [Required(ErrorMessage = "Please name the allegies or write 'N/A' if not applicable")]
        public string AllegDe { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Is the patient taking any prescription medication (including contraceptive pills, liver enzymes inducing drugs, oral glucocorticoids, OTC medication)? if yes provide details")]
        public string PreMed { get; set; }
        [Required(ErrorMessage = "Please name the medications or write 'N/A' if not applicable")]
        public string PreMedDe { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Acute liver disease? ")]
        public string LiverDis { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Breastfeeding? ")]
        public string BreastF { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Currently vomiting or diarrhea? ")]
        public string Vomit { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Emergency Hormonal Contraceptive supplied to patient?")]
        public string ContracName { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Select the name of EHC provided / Not Applicable EHC is not supplied ")]
        public string ContracNameD { get; set; }
    }
}
