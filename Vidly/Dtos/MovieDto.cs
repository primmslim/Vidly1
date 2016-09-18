using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }


        public Byte GenreId { get; set; }


        public Genre Genre { get; set; }


        public DateTime ReleaseDate { get; set; }


        public DateTime DateAdded { get; set; }


        public byte StockCount { get; set; }

    }
}