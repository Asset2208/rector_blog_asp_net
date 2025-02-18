﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rector_blog.Models;
using rector_blog.Filters;
using Newtonsoft.Json;

namespace rector_blog.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comment
        [CustomAuth(Roles = "Admin")]
        public ActionResult Index()
        {
            var commentModels = db.CommentModel.Include(c => c.BlogPostModels);
            return View(commentModels.ToList());
        }

        // GET: Comment/Details/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModels commentModels = db.CommentModel.Include(c => c.BlogPostModels).FirstOrDefault(c => c.Id == id);
            //CommentModels commentModels = db.CommentModel.Include(c => c.BlogPostModels).FirstOrDefault(c => c.Id == id);
            if (commentModels == null)
            {
                return HttpNotFound();
            }
            return View(commentModels);
        }

        // GET: Comment/Create
        [Authorize(Roles ="Admin,User")]
        public ActionResult Create()
        {
            ViewBag.BlogPostModelsId = new SelectList(db.BlogPostModel, "Id", "Title");
            return View();
        }
        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }


        // POST: Comment/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Is_reply_to_id,Content,User_id,Enabled,Created_date,BlogPostModelsId")] CommentModels commentModels)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success && ModelState.IsValid)
            {
                commentModels.ApplicationUser = db.Users.Find(commentModels.User_id);
                db.CommentModel.Add(commentModels);
                db.SaveChanges();
                return RedirectToAction("Details", "BlogPosts", new { id = commentModels.BlogPostModelsId});
            }
            else
            {
                return Content("Error From Google ReCaptcha : " + response.ErrorMessage[0].ToString());
            }

            ViewBag.BlogPostModelsId = new SelectList(db.BlogPostModel, "Id", "Title", commentModels.BlogPostModelsId);
            return View(commentModels);
        }

        // GET: Comment/Edit/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModels commentModels = db.CommentModel.Find(id);
            if (commentModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_id = commentModels.User_id;
            ViewBag.BlogPostModelsId = new SelectList(db.BlogPostModel, "Id", "Title", commentModels.BlogPostModelsId);
            return View(commentModels);
        }

        // POST: Comment/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Is_reply_to_id,Content,User_id,Enabled,Created_date,BlogPostModelsId")] CommentModels commentModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogPostModelsId = new SelectList(db.BlogPostModel, "Id", "Title", commentModels.BlogPostModelsId);
            return View(commentModels);
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModels commentModels = db.CommentModel.Find(id);
            if (commentModels == null)
            {
                return HttpNotFound();
            }
            return View(commentModels);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModels commentModels = db.CommentModel.Find(id);
            db.CommentModel.Remove(commentModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
