using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieApp.Manegers;
using MovieApp.Models;
using PagedList;
using X.PagedList.Mvc.Bootstrap4;

namespace MovieApp.Controllers
{
    public class StoreMoviesController : Controller
    {
        // GET: StoreMovies
        public ActionResult Index(int? page,string storeName=null)
        {
           
            StoreMoviesManeger maneger=new StoreMoviesManeger();
            List<StoreMovies> movieses = maneger.GetAllStoreMovies(storeName);
            if (!page.HasValue)
            
                page = 1;
                int TotalCount = movieses.Count, PageSize = 5;
                ViewBag.CurrentPage = page;
                ViewBag.TotalCount = TotalCount;
                ViewBag.PageSize = PageSize;

                if (TotalCount<=PageSize)
                {
                    return View(movieses);
                }
                
            

           List<StoreMovies> storeMovieses = movieses.Skip((page.Value - 1) * PageSize).Take(PageSize).ToList();
           return View(storeMovieses);

        }
        [HttpGet]
        public ActionResult Search(string storeName)
        {
            StoreMoviesManeger maneger = new StoreMoviesManeger();
            List<StoreMovies> storeList = maneger.GetAllStoreMovies(storeName);
            return Json(storeList,JsonRequestBehavior.AllowGet);
           
        }

        [HttpGet]
        public ActionResult Create()
        {
            StoreMenager maneger=new StoreMenager();
            List<Store> stores = maneger.GetAllStore();
            ViewBag.Store = new SelectList(stores,"StoreId","StoreName");

            MovieManeger movieManeger=new MovieManeger();
            List<Movie> movies = movieManeger.GetAllMovies();
            ViewBag.Movie = new SelectList(movies, "ID", "Title");
            return View();
        }

        [HttpPost]
        public ActionResult Create(StoreMovies storeMovies)
        {
            storeMovies.StoreId = storeMovies.StoreId;
            storeMovies.Quintity = storeMovies.Quintity;
            storeMovies.RelaseDate = storeMovies.RelaseDate.Date;
            var  maneger=new StoreMoviesManeger();
            
            bool saved = maneger.InsertMovies(storeMovies);
            if (saved)
            {
                TempData["Success"] = "Store Movie Save Successfullly..";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Save Error";
                return View(storeMovies);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            LoadDropDwon();
            StoreMoviesManeger maneger = new StoreMoviesManeger();
            
            var storeMovie = maneger.GetStoreMovieByID(id);
            if (storeMovie==null)
            {
                return RedirectToAction("NotFound404","Error");
            }
            return View(storeMovie);
        }
        [HttpPost]
        public ActionResult Edit(StoreMovies storeMovies)
        {
            StoreMoviesManeger maneger=new StoreMoviesManeger();
            bool Update = maneger.UpdateStoreMovie(storeMovies);
            if (Update)
            {
                TempData["Update"] = "Update Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                LoadDropDwon();
                TempData["Error"] = "Can't Data Updated";
                return View(storeMovies);
            }
        }

        private void LoadDropDwon()
        {
            StoreMenager storeMenager = new StoreMenager();
            List<Store> stores = storeMenager.GetAllStore();
            ViewBag.Stores = new SelectList(stores, "StoreId", "StoreName");

            MovieManeger movieManeger = new MovieManeger();
            List<Movie> movies = movieManeger.GetAllMovies();
            ViewBag.MovieTitle = new SelectList(movies, "ID", "Title");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            StoreMoviesManeger maneger=new StoreMoviesManeger();
            var StoreMovie = maneger.GetStoreMovieByID(id);
            return View(StoreMovie);
        }

        [HttpPost]
        public ActionResult Delete(StoreMovies storeMovies)
        {
            StoreMoviesManeger maneger=new StoreMoviesManeger();
            bool StoreMovie = maneger.DeleteStoreMovie(storeMovies);
            if (StoreMovie)
            {
                TempData["Delete"] = "Delete Data Sucessfully";
                return RedirectToAction("Index");

            }
            else
            {
                
                TempData["Error"] = "Data can't Delete";
                return View(storeMovies);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            StoreMoviesManeger maneger=new StoreMoviesManeger();
            var storeMovie = maneger.GetStoreMovieByID(id);
            return View(storeMovie);
        }

        [HttpPost]
        public ActionResult Details(StoreMovies storeMovies)
        {
            StoreMoviesManeger maneger=new StoreMoviesManeger();
            maneger.DetailsStoreMovies(storeMovies);
            return RedirectToAction("Details");
        }
    }
}