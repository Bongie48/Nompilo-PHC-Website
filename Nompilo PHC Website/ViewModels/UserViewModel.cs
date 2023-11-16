using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Nompilo_PHC_Website.ViewModels
{
    public class UserViewModel
    {

        public string? Id { get; set; }

        public string Name { get; set; }


     
        public string Surname { get; set; }

      
        public string Email { get; set; }

        [DisplayName("Roles")]

        public IList<SelectListItem>? Roles { get; set; }
    }
}
