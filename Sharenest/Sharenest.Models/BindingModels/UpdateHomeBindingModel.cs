using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sharenest.Models.BindingModels
{
    public class UpdateHomeBindingModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public string LocationName { get; set; }

        [Required]
        public string Activities { get; set; }

        [Required]
        public string Provision { get; set; }

        public string Notes { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
