using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //Get api/Movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
                
        }


        //Get api/Movies/1

        public Movie GetMovie(int ID)
        {
            var Movie = _context.Movies.SingleOrDefault(m => m.ID == ID);
            if (Movie == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return Movie;
        }

        // Delete /api/movie/id

        [HttpDelete]
        public void DeleteMovie(int ID)
        {
            var Movie = _context.Movies.SingleOrDefault(m => m.ID == ID);
            if (Movie == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(Movie);
            _context.SaveChanges();

        }

        [HttpPost]
        //Add /api/movie
        public MovieDto CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.ID = movie.ID;
            return movieDto;
        }

        [HttpPut]
        public void EditMovie(MovieDto movieDto, int ID)
        {
            if (!ModelState.IsValid) throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movie = _context.Movies.SingleOrDefault(m => m.ID == ID);
            if (movie == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<MovieDto, Movie>(movieDto, movie);
            _context.SaveChanges();
            


        }
    }
}