using System.ComponentModel.DataAnnotations;

namespace Nompilo_PHC_Website.Models
{
    public class WaitList
    {
        [Key]
        public int WaitID { get; set; }

        public virtual Booking? Bookings { get; set; }


    }
}
