using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieApp.Manegers;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index( int page=1,int pageSize=10, string title=null)
        {

            MovieManeger maneger=new MovieManeger();
            Tuple<List<Movie>, int> movTpl = maneger.PageListMovies(page,pageSize,title);
            ViewBag.TotalRow = movTpl.Item2;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
            return View(movTpl.Item1);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            movie.Title = movie.Title;
            movie.ReleaseDate = Convert.ToDateTime(movie.ReleaseDate);
            movie.Genre = movie.Genre;
            movie.Price = movie.Price;
           
            MovieManeger maneger=new MovieManeger();
            bool hasMovie = maneger.HasMovieTitle(movie.Title);
            if (hasMovie)
            {
                TempData["Exist"] = "Movie Already Exist";
                return View(movie);
            }
            bool saved = maneger.InsertMovie(movie);

            if (saved)
            {
                TempData["Success"] = "Save Sucessful";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Can't Inserted";
            return View(movie);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            MovieManeger maneger=new MovieManeger();
            var movie = maneger.GetMovieById(id);
            return View(movie);
        }
        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            MovieManeger maneger=new MovieManeger();
            bool Update = maneger.UpdateMovie(movie);
            if (Update)
            {
                TempData["Update"] = "Update Sucessful";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Can't Update";
                return View(movie);
            }

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var maneger=new MovieManeger();
            var movie = maneger.GetMovieById(id);
            return View(movie);
        }
        [HttpPost]
        public ActionResult Delete(Movie movie)
        {
            var maneger=new MovieManeger();
            bool delete = maneger.DeleteMovie(movie);

            if (delete)
            {
                TempData["Delete"] = "Delete Successful";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Data can't Delete";
                return View(movie);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var maneger=new MovieManeger();
            var movie = maneger.GetMovieById(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult Details(Movie movie)
        {
            var  maneger=new MovieManeger();
            maneger.Details(movie);
            return RedirectToAction("Details");
        }


    }
}