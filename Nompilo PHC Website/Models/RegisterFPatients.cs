using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Nompilo_PHC_Website.Models
{
    public class RegisterFPatients
    {
        [Key]
        public int RegId { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        [DisplayName("Is the patient registered for regular contraceptives? ")]
        public string CheckRegister { get; set; }

        //prenatal 
        public string? UseId { get; set; }


    }
}
