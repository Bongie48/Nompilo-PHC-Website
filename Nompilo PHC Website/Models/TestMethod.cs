using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class TestMethod
    {
        [Key]
        public int methodId { get; set; }

        [Required]
        [DisplayName("Method Name")]
        public string MethodName { get; set; }

        [DisplayName("Test For")]
        [ForeignKey("testId")]
        public int testId { get; set; }
        public virtual Test? Tests { get; set; }

        [DisplayName("Instrument")]
        [ForeignKey("instrumentId")]
        public int instrumentId { get; set; }
        public virtual Instrument? Instruments { get; set; }

        [Required(ErrorMessage ="Please elaborate on how the test method is used")]
        [DisplayName("Method Description")]
        public string Description { get; set; }

        public string status { get; set; }
    }
}
