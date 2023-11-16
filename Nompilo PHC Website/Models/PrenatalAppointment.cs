using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nompilo_PHC_Website.Models
{
    public class PrenatalAppointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        public int BookingId { get; set; }

        [ForeignKey("BookingId")]
        public Booking Booking { get; set; }
    }
}
