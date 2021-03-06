﻿using ProjectDAW.Models;
using ProjectDAW.ViewModels;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace ProjectDAW.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            TblPost post = _context.TblPost
                                   .FirstOrDefault(p => p.PostId == id);

            List<Comment> comments = post.TblComments.Select(c => new Comment {
                CommentId = c.CommentId,
                PostId = c.PostId,
                Content = c.Content
            }).ToList();

            var model = new Post
            {
                Title = post.Title,
                PostId = post.PostId,
                Content = post.Content,
                CategoryId = post.CategoryId,
                Comments = comments
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int categoryId)
        {
            ViewBag.Title = "Create Post";
            ViewBag.SubmitAction = "Create Post";
            return View(nameof(Edit), new Post { CategoryId = categoryId });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var post = _context.TblPost.Find(id);

            var model = new Post
            {
                Title = post.Title,
                PostId = post.PostId,
                Content = post.Content,
                CategoryId = post.CategoryId
            };

            ViewBag.Title = "Update Post";
            ViewBag.SubmitAction = "Update Post";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), post);
            }

            TblPost dbPost = _context.TblPost.Find(post.PostId);

            if (dbPost == null)
            {
                dbPost = new TblPost();
                dbPost.CreatedDate = DateTime.Now;
                dbPost.CategoryId = post.CategoryId;
            }

            dbPost.Title = post.Title;
            dbPost.Content = post.Content;

            if (!TryValidateModel(dbPost))
            {
                return View(nameof(Edit), post);
            }

            _context.TblPost.AddOrUpdate(dbPost);
            _context.SaveChanges();

            TempData["message"] = "Post Updated/Created!";
            TempData["messageColorClass"] = "alert-success";
            return RedirectToAction(nameof(Show), new { id = dbPost.PostId });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int categoryId)
        {
            var post = _context.TblPost.Find(id);
            _context.TblPost.Remove(post);
            _context.SaveChanges();

            TempData["message"] = "Post Deleted!";
            TempData["messageColorClass"] = "alert-danger";
            return RedirectToAction(nameof(CategoryController.Show), "Category", new { id = categoryId});
        }
    }
}