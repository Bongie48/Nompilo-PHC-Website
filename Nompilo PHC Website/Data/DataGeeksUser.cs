using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Nompilo_PHC_Website.Models;

namespace Nompilo_PHC_Website.Data
{
    public class DataGeeksUser:IdentityUser
    {
       
        //public int userId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
        public ICollection<TestResult>? TestResults { get; set; }
    }
}
