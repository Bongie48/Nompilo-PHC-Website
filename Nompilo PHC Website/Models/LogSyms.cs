using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nompilo_PHC_Website.Models
{
    public class LogSyms
    {
        [Key]
        public int SymId { get; set; }
        [Required(ErrorMessage = "Please select the day you want record symptoms for")]
        public DateTime Day { get; set; }
        [Required(ErrorMessage = "Please select from options provided")]
        public string Nausea { get; set; }
        [Required(ErrorMessage = "Please select from options provided")]
        [DisplayName("Weight Gained? ")]
        public string Weight { get; set; }
        [Required(ErrorMessage = "Please select from options provided")]
        [DisplayName("Missed period? ")]
        public string period { get; set; }
        [Required(ErrorMessage = "Please select from options provided")]
        [DisplayName("Do you notice any signs of vaginal yeast Infection (thrush)? ")]
        public string vaginal { get; set; }
        [Required(ErrorMessage = "Please select from options provided")]
        [DisplayName("Are Experiencing Any Breakthrough Bleeding (Spotting in between periods) ? ")]
        public string Spotting { get; set; }
        [Required(ErrorMessage = "Please write your symptoms")]
        public string Symptoms { get; set; }

        public string EmailAdd { get; set; }
    }
}
