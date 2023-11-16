using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class Instrument
    {

        [Key]
        public int instrumentId { get; set; }

        
        [DisplayName("Instrument Name")]
        [Required(ErrorMessage = "Please provide instrument name!")]
        public string? name { get; set; }

        
        [DisplayName("Instrument Description")]
        [Required(ErrorMessage = "Please provide instrument description!")]
        public string? description { get; set; }

        [DisplayName("Test For")]
        [ForeignKey("testId")]
        public int testId { get; set; }
        public virtual Test? Tests { get; set; }

        public string? status { get; set; }

        public ICollection<TestMethod>? TestMethods { get; set; }
    }

}
