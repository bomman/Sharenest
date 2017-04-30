using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sharenest.Models.EntityModels
{
    [Table("Pictures")]
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        public string SmallPicturePath { get; set; }

        public string MediumPicturePath { get; set; }

        public string LargePicturePath { get; set; }
    }
}
