using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vidly.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public Byte GenreId { get; set; }


        public Genre Genre { get; set; }

        [Required]
        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name ="Date Added")]
        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1,20)]
        [Display(Name ="Stock Count")]
        public byte StockCount { get; set; }



    }
}
