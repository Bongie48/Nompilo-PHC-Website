using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nompilo_PHC_Website.Models
{
    public class CheckType
    {
        [Key]
        public int RegId { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Is the patient here for emergency hormonal contraceptive or regular contraceptive? ")]
        public string CheckRegister { get; set; }
    }
}
