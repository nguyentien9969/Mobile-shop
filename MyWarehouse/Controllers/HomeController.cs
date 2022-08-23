using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWarehouse.Controllers
{
    public class HomeController : BasicAuthorizationController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Product");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _LeftMenu()
        {
            return View();
        }
        
        public ActionResult _TopMenu()
        {
            return View();
        }

        public ActionResult _Footer()
        {
            return View();
        }
    }
}