﻿using System;

namespace Sharenest.Models.ViewModels.Homes
{
    public class HomesIndexViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string LocationName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Rating { get; set; }
    }
}
