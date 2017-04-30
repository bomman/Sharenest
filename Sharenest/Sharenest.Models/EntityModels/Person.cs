using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sharenest.Models.Enums;

namespace Sharenest.Models.EntityModels
{
    [Table("Persons")]
    public class Person
    {
        public Person()
        {
            this.Pictures = new HashSet<Picture>();
            this.VisitedPlaces = new HashSet<Home>();
        }

        public virtual ApplicationUser User { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public GenderEnum Gender { get; set; }

        public virtual Location Location { get; set; }

        public string Interests { get; set; }

        public virtual Picture ProfilePicture { get; set; }

        [DefaultValue(0)]
        [Range(0, 10)]
        public int Rating { get; set; }

        public ICollection<Picture> Pictures { get; set; }

        public ICollection<Home> VisitedPlaces { get; set; }
    }
}
