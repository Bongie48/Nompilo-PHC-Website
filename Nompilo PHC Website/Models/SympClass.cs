using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Nompilo_PHC_Website.Models
{
    public class SympClass
    {
        [Key]
        public int SyID { get; set; }

        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Did the patient log symptoms:")]
        public string QSym { get; set; }
    }
}
