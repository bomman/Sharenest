using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sharenest.Models.EntityModels;

namespace Sharenest.Models.ViewModels.Homes
{
    public class HomeDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProfilePicture { get; set; }

        public Location Location { get; set; }

        [Required]
        [DisplayName("What do we offer?")]
        public string Activities { get; set; }

        [Required]
        [DisplayName("What do we have?")]
        public string Provision { get; set; }

        public string Notes { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PostedDate { get; set; }

        public List<string> PicturesMedium { get; set; }

        public int Rating { get; set; }
    }
}
