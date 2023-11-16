using System.ComponentModel.DataAnnotations;

namespace Nompilo_PHC_Website.Models
{
    public class InstrumentRequest
    {
        [Key]
        public int requestId { get; set; }

        [Required(ErrorMessage ="Instrument Name")]
        public string? name { get; set; }

        [Required(ErrorMessage = "Image")]
        public string? ImageUrl { get; set; }

    }
}
