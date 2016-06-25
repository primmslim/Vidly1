using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        public ActionResult Edit(int Id)
        {
            return Content("Id = " + Id);
        }

        public ActionResult Index(int? PageIndex, string SortBy)
        {
            if (!PageIndex.HasValue)
                PageIndex = 1;
            if (string.IsNullOrWhiteSpace(SortBy))
                SortBy = "Name";
            return View(GetMovies());
        }
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content("Year: " + year + " Month: " + month);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie {ID = 1, Name = "Matix" },
                new Movie {ID = 2, Name = "Wall-e" },
                new Movie {ID = 3, Name = "Shrek 3" }
            };

        }

        public ActionResult Details(int id)
        {
            var movie = GetMovies().SingleOrDefault(m => m.ID == id);
            if (movie == null) return HttpNotFound();
            return View(movie);
        }
    }
}