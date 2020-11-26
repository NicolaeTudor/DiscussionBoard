using ProjectDAW.Models;
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
    public class CommentController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            TblComment comment = _context.TblComment
                                   .FirstOrDefault(c => c.CommentId == id);

            var model = new Comment
            {
                CommentId = comment.CommentId,
                Content = comment.Content,
                PostId = comment.PostId
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int postId)
        {
            ViewBag.Title = "Create Comment";
            ViewBag.SubmitAction = "Create Comment";
            return View(nameof(Edit), new Comment());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var comment = _context.TblComment.Find(id);

            var model = new Comment
            {
                CommentId = comment.CommentId,
                Content = comment.Content,
                PostId = comment.PostId
            };

            ViewBag.Title = "Update Comment";
            ViewBag.SubmitAction = "Update Comment";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrEdit(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), comment);
            }

            TblComment dbComment = _context.TblComment.Find(comment.CommentId);

            if (dbComment == null)
            {
                dbComment = new TblComment();
                dbComment.CreatedDate = DateTime.Now;
                dbComment.PostId = comment.PostId;
            }

            dbComment.Content = comment.Content;

            if (!TryValidateModel(dbComment))
            {
                return View(nameof(Edit), comment);
            }

            TempData["message"] = "Comment Updated/Created!";
            TempData["messageColorClass"] = "alert-success";

            _context.TblComment.AddOrUpdate(dbComment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Show), new { id = dbComment.CommentId });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int postId)
        {
            var comment = _context.TblComment.Find(id);
            _context.TblComment.Remove(comment);
            _context.SaveChanges();

            TempData["message"] = "Comment Deleted!";
            TempData["messageColorClass"] = "alert-danger";
            return RedirectToAction(nameof(PostController.Show), "Post", new { id = postId});
        }
    }
}