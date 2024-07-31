using System.ComponentModel.DataAnnotations;

namespace dwtDotNet.Models
{
    public class ImageviewModel
    {
        public int Id { get; set; }

        [Display(Name = "title of image")]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Title { get; set; }

        [Display(Name = "Photo")]
        public string ImageId { get; set; }
    }
}
