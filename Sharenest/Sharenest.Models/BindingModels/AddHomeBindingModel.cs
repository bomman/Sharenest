using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Sharenest.Models.BindingModels
{
    public class AddHomeBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string LocationName { get; set; }

        [Required]
        public string Activities { get; set; }

        [Required]
        public string Provision { get; set; }

        public string Notes { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public HttpPostedFileBase ProfilePicture { get; set; }
    }
}
