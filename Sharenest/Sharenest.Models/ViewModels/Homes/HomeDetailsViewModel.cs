using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sharenest.Models.EntityModels;

namespace Sharenest.Models.ViewModels.Homes
{
    public class HomeDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        [Required]
        public string Activities { get; set; }

        [Required]
        public string Provision { get; set; }

        public string Notes { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? PostedDate { get; set; }

        public List<string> PicturesMedium { get; set; }

        public int Rating { get; set; }
    }
}
