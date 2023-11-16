using Microsoft.AspNetCore.Mvc;
using Nompilo_PHC_Website.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string services { get; set; }

        [Required]
        [BindProperty]
        public DateTime datetimes { get; set; }

        public string status { get; set; }

        public string? DataGeeksUserId { get; set; }

        [ForeignKey("DataGeeksUserId")]
        public DataGeeksUser? Patients { get; set; }
    }
}
