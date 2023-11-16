using Nompilo_PHC_Website.Data;
using Nompilo_PHC_Website.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Nompilo_PHC_Website.ViewModels
{
    public class TestResultVM
    {
        
        public string UserId { get; set; }
        public virtual DataGeeksUser? User { get; set; }

        [Required(ErrorMessage = "Please give a detailed results description.")]
        public string? Description { get; set; }

        [DisplayName("Test For")]
        [ForeignKey("testId")]
        public int testId { get; set; }
        public virtual Test? Tests { get; set; }

        [DisplayName("Test Method")]
        [ForeignKey("testmethodId")]
        public int testmethodId { get; set; }
        public virtual TestMethod? TestMethods { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime date { get; set; }


        [Required(ErrorMessage = "Please provide test results outcome.")]
        public string? Result { get; set; }

    }
}
