using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieApp.Manegers;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            var maneger = new StoreMenager();
            List<Store> stores=maneger.GetAllStore();
            return View(stores);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Store store)
        {
            store.StoreName = store.StoreName;
            store.Address = store.Address;
            store.City = store.City;
            store.ZipCode = store.ZipCode;
            store.Country = store.Country;
            var maneger=new StoreMenager();
            bool saved = maneger.InsertStore(store);
            if (saved)
            {
                TempData["Success"] = "Data Save Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Data Can't Inserted";
                return View(store);
            }

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var maneger=new StoreMenager();
            var store = maneger.GetStoreById(id);

            return View(store);
        }

        [HttpPost]
        public ActionResult Edit(Store store)
        {
            var maneger=new StoreMenager();
            bool update = maneger.UpdateStore(store);
            if (update)
            {
                TempData["Update"] = "Store Successfully Updated..";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Store Can't Updated..";
                return View(store);
            }
            
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var maneger = new StoreMenager();
            var store = maneger.GetStoreById(id);

            return View(store);
        }
        [HttpPost]
        public ActionResult Delete(Store store)
        {
            var maneger = new StoreMenager();
            bool delete = maneger.DeleteStore(store);
            if (delete)
            {
                TempData["Delete"] = "Store Successfully Deleted..";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Store Can't Delete..";
                return View(store);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var maneger=new StoreMenager();
            var store = maneger.GetStoreById(id);
            return View(store);
        }
        [HttpPost]
        public ActionResult Details(Store store)
        {
            var maneger=new StoreMenager();
            maneger.Details(store);
            return RedirectToAction("Details");
        }
    }
}