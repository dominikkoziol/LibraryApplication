using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field Name is required")]
        [StringLength(255)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field Author Name is required")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Field Genre is required")]
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime DateAdded { get; set; }
        [Required(ErrorMessage = "Field ReleaseDate is required")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Field NumberInStock is required")]
        [Range(1, 20, ErrorMessage = "NumberInStock must be between 1 and 20.")]
        public int NumberInStock { get; set; }
        [Required(ErrorMessage = "Field NumberAvailable is required")]

        public int NumberAvailable { get; set; }
    }

}
