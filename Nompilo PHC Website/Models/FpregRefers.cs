using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nompilo_PHC_Website.Models
{
    public class FpregRefers
    {
        [Key]
        public int RefId { get; set; }
        [Required(ErrorMessage = "Please provide patient first name")]
        [DisplayName("First Name")]
        public string RefName { get; set; }
        [Required(ErrorMessage = "Please provide patient last name")]
        [DisplayName("Last Name")]
        public string RefLastName { get; set; }
        [Required(ErrorMessage = "Please select the date at which the patient is being referred to pretenal")]
        [DisplayName("Reference Date")]
        public DateTime RefDate { get; set; } 
    }
}
