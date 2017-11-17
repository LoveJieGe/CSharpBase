using Chapter33_BlogDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chapter33_BlogWep.Controllers
{
    public class HomeController : Controller
    {
        IBlogDal blogDal = new BlogDal();
        public ActionResult Index()
        {
            var blogs = blogDal.GetEntities(b => true);
            return View(blogs);
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
    }
}