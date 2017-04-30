using System.ComponentModel.DataAnnotations;

namespace Sharenest.Models.ViewModels.Manage
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}