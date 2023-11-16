using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Nompilo_PHC_Website.Models
{
    public class FPregnancy
    {
        [Key]
        public int PregId { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        public string Results { get; set; }
        
    }
}
