using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sharenest.Models.EntityModels;

namespace Sharenest.Models.ViewModels.Homes
{
    public class HomeEditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        [Required]
        public string Activities { get; set; }

        [Required]
        public string Provision { get; set; }

        public string Notes { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? PostedDate { get; set; }

        public List<string> PicturesMedium { get; set; }

        public int Rating { get; set; }
    }
}
