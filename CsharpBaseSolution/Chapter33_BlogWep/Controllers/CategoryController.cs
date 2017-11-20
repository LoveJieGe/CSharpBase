using Chapter33_BlogDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chapter33_BlogWep.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryDal dal = new CategoryDal();
        // GET: Category
        public ActionResult Index()
        {
            var categories = dal.GetEntities().ToList();
            return View(categories);
        }

        // GET: /Category/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Chapter33_BlogLib.Category entity)
        {
            try
            {
                dal.InsertEntity(entity);
                dal.SaveChange();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Chapter33_BlogLib.Category entity = dal.GetEntity(c => c.CategoryID == id);
                dal.DeleteEntity(entity);
                dal.SaveChange();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                throw new Exception();
            }
           
        }
    }
}