using Microsoft.AspNetCore.Identity;

namespace Nompilo_PHC_Website.Models
{
    public class Registration : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int IDnumber { get; set; }

        public string Address { get; set; }

        public string Title { get; set; }



    }
}
