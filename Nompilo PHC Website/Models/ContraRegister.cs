using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace Nompilo_PHC_Website.Models
{
    public class ContraRegister
    {
        [Key]
        public int RegId { get; set; }
        [Required]
        [StringLength(13)]
        
        [Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
    }
}
