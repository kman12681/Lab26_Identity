using Lab_26.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab_26.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            CoffeeEntities orm = new CoffeeEntities();

            ViewBag.Items = orm.Items.ToList();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


    }
}
