using Nompilo_PHC_Website.Models;
using System.ComponentModel.DataAnnotations;
namespace Nompilo_PHC_Website.Models
{
    public class Report_Wlk
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string NormalizedEmail { get; set; }

        public string EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneNumberConfirmed { get; set; }
    }
}
