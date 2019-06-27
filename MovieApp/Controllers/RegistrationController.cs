using MovieApp.Manegers;
using MovieApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieApp.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveData(Registration reg)
        {
            var UserName =reg.UserName;
            var Email = reg.Email;
            var Password = reg.Password;
            RegistrationManeger maneger = new RegistrationManeger();
            maneger.InsertRegistration(reg);

            return Json(reg,JsonRequestBehavior.AllowGet);
        }
    }
}