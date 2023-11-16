using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class MigTest
    {

        [Key]
        public int instId { get; set; }


        [DisplayName("Name")]
        [Required(ErrorMessage = "Please provide name!")]
        public string? Sname { get; set; }


        [DisplayName("Description")]
        [Required(ErrorMessage = "Please provide description! ")]
        public string? Migdescription { get; set; }
    }
}
