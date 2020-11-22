using ProjectDAW.Models;
using ProjectDAW.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDAW.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();
        // GET: Category
        public ActionResult Index()
        {
            var categories = _context.TblCategory
                                     .Select(c => new Category {
                                         Name = c.Name,
                                         CategoryId = c.CategoryId,
                                         Description = c.Description
                                     });

            return View(categories);
        }

        public ActionResult Show(int id)
        {
            var category = _context.TblCategory.Find(id);

            var model =  new Category{
                            Name = category.Name,
                            CategoryId = category.CategoryId,
                            Description = category.Description
                         };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Create Category";
            ViewBag.SubmitAction = "Create Category";
            return View(nameof(Edit), new Category());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = _context.TblCategory.Find(id);

            var model = new Category
            {
                Name = category.Name,
                CategoryId = category.CategoryId,
                Description = category.Description
            };

            ViewBag.Title = "Update Category";
            ViewBag.SubmitAction = "Update Category";

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateOrEdit(Category category)
        {
            TblCategory dbCategory = _context.TblCategory.Find(category.CategoryId);

            if(dbCategory == null)
            {
                dbCategory = new TblCategory();
            }

            dbCategory.Description = category.Description;
            dbCategory.Name = category.Name;

            _context.TblCategory.AddOrUpdate(dbCategory);
            _context.SaveChanges();

            return RedirectToAction(nameof(Show), new { id = dbCategory.CategoryId });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = _context.TblCategory.Find(id);
            _context.TblCategory.Remove(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}