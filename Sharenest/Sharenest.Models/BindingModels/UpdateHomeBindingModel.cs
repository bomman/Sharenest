using System;
using System.ComponentModel.DataAnnotations;

namespace Sharenest.Models.BindingModels
{
    public class UpdateHomeBindingModel
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
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }
    }
}
