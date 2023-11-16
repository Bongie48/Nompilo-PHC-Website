using Microsoft.AspNetCore.Identity;
using Nompilo_PHC_Website.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class TestResult
    {
        [Key]
        public int resultsId { get; set; }


        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual DataGeeksUser? User { get; set; }

        [DisplayName("More information")]
        [Required(ErrorMessage = "Please give a detailed information on results.")]
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
        [Required(ErrorMessage ="Please select date")]
        [DisplayName("Date")]
        public DateTime date { get; set; }


        [Required(ErrorMessage ="Please provide test results outcome.")]
        public string? Result { get; set; }

        public string status { get; set; }
    }
}
