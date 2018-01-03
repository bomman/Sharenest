﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Sharenest.Models.ViewModels.Homes
{
    public class HomesIndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProfilePicture { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PostedDate { get; set; }

        public int Rating { get; set; }
    }
}
