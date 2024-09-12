using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NileconTest.Models
{
    public class ContentModel
    {
        [Required]
        [DisplayName("ID")]
        public int contentID {  get; set; }

        [Required]
        [DisplayName("Title")]
        public string contentTitle { get; set; }

        [Required]
        [DisplayName("Image (URL)")]
        public string contentImage { get; set; }

        [Required]
        [DisplayName("Description")]
        public string contentDescription { get; set; }
    }
}
