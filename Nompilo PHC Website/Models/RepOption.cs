using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace Nompilo_PHC_Website.Models
{
    public class RepOption
    {
        [Key]
        public int RepId { get; set; }
        [Required(ErrorMessage = "Please select from the options provided")]
        public string ReportOption { get; set; }

    }
}
