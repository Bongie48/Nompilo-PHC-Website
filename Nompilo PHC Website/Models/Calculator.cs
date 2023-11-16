using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nompilo_PHC_Website.Models
{
    public class Calculator
    {
        [Required]
        [BindProperty]
        public DateTime Conception { get; set; }
    }
}
