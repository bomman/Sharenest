using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sharenest.Models.ViewModels.Admin
{
    public class AdminHomesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string LocationName { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PostedDate { get; set; }
    }
}
