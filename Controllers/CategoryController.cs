﻿using ProjectDAW.Models;
using ProjectDAW.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            TblCategory category = _context.TblCategory
                                           .Include(c => c.TblPost)
                                           .FirstOrDefault(c => c.CategoryId == id);

            List<PostListItem> categoryPosts = category.TblPost.Select(p => new PostListItem {
                PostId = p.PostId,
                Title = p.Title,
                Content = p.Content
            }).ToList();

            var model = new Category{
                Name = category.Name,
                CategoryId = category.CategoryId,
                Description = category.Description,
                CategoryPosts = categoryPosts
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
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), category);
            }

            TblCategory dbCategory = _context.TblCategory.Find(category.CategoryId);

            if(dbCategory == null)
            {
                dbCategory = new TblCategory();
            }

            dbCategory.Description = category.Description;
            dbCategory.Name = category.Name;

            if (!TryValidateModel(dbCategory))
            {
                return View(nameof(Edit), category);
            }

            _context.TblCategory.AddOrUpdate(dbCategory);
            _context.SaveChanges();

            TempData["message"] = "Category Updated/Created!";
            TempData["messageColorClass"] = "alert-success";

            return RedirectToAction(nameof(Show), new { id = dbCategory.CategoryId });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = _context.TblCategory.Find(id);
            _context.TblCategory.Remove(category);
            _context.SaveChanges();

            TempData["message"] = "Category Deleted!";
            TempData["messageColorClass"] = "alert-danger";
            return RedirectToAction(nameof(Index));
        }
    }
}