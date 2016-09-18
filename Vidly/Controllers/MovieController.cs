using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        public MovieController()
        {
            _context = new ApplicationDbContext();
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
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
            var movie = _context.Movies.Single(m => m.ID == Id);
            if (movie == null) return HttpNotFound();

            var viewModel = new EditMovieViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };
            return View("Edit", viewModel);
        }

        public ActionResult Index(int? PageIndex, string SortBy)
        {
            if (!PageIndex.HasValue)
                PageIndex = 1;
            if (string.IsNullOrWhiteSpace(SortBy))
                SortBy = "Name";

            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(_context.Movies);
        }
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content("Year: " + year + " Month: " + month);
        }


        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(g => g.Genre).SingleOrDefault(m => m.ID == id);
    
            if (movie == null) return HttpNotFound();
            return View(movie);
        }

        public ActionResult Create()
        {

            var viewModel = new EditMovieViewModel
            {
                Movie = new Movie
                {
                    DateAdded = DateTime.Today,
                    StockCount = 1
                }
            };
            viewModel.Genres = _context.Genres.ToList();
            return View("Edit",viewModel);
            
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EditMovieViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var vm = new EditMovieViewModel
                {
                    Movie = viewModel.Movie,
                    Genres = _context.Genres.ToList()
                };
                return View("Edit", vm);
            }

            if (viewModel.Movie.ID == 0)
            {
                _context.Movies.Add(viewModel.Movie);
            }else
            {
                var movieInDb = _context.Movies.Include("Genre").Single(m => m.ID == viewModel.Movie.ID);
                movieInDb.Name = viewModel.Movie.Name;
                movieInDb.GenreId = viewModel.Movie.GenreId;
                movieInDb.DateAdded = viewModel.Movie.DateAdded;
                movieInDb.ReleaseDate = viewModel.Movie.ReleaseDate;
                movieInDb.StockCount = viewModel.Movie.StockCount;

            }
          
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}