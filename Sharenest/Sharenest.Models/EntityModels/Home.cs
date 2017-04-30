using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharenest.Models.EntityModels
{
    [Table("Homes")]
    public class Home
    {
        public Home()
        {
            this.Pictures = new HashSet<Picture>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Location Location { get; set; }

        [Required]
        public string Activities { get; set; }

        [Required]
        public string Provision { get; set; }

        public string Notes { get; set; }

        public bool IsVisited { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime PostedDate { get; set; }

        [DefaultValue(0)]
        [Range(0, 10)]
        public int Rating { get; set; }

        public ICollection<Picture> Pictures { get; set; }
    }
}
