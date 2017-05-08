using System.Collections.Generic;
using System.ComponentModel;

namespace Sharenest.Models.ViewModels.Admin
{
    public class AdminPersonsViewModel
    {
        public string Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [DisplayName("Role")]
        public AdminRoleViewModel Role { get; set; }
    }
}
