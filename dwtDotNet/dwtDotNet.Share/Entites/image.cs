using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dwtDotNet.Share.Entites
{
    public class image
    {
        public int Id { get; set; }

        [Display(Name = "title of image")]
        [MaxLength(100, ErrorMessage = "The field {0} must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Title { get; set; }

        [Display(Name = "Photo")]
        public string ImageId { get; set; }

        //TODO: Pending to change to the correct path
        [Display(Name = "Photo")]
        public string ImageFullPath => ImageId == string.Empty
            ? $"https://localhost:7084/images/noimage.png"
            : $"https://localhost:7084/{ImageId}";
    }
}
