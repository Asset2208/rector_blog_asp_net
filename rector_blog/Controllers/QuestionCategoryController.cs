using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rector_blog.Models;

namespace rector_blog.Controllers
{
    public class QuestionCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionCategory
        public ActionResult Index()
        {
            return View(db.QuestionCategoryModel.ToList());
        }

        // GET: QuestionCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //QuestionBlogPostModels questionBlogPostModels = db.QuestionBlogPostModel.Include(q => q.QuestionCategories).FirstOrDefault(q => q.ID == id);

            QuestionCategoryModels questionCategoryModels = db.QuestionCategoryModel.Include(q => q.QuestionBlogPosts).FirstOrDefault(q => q.ID == id);
            if (questionCategoryModels == null)
            {
                return HttpNotFound();
            }
            return View(questionCategoryModels);
        }

        // GET: QuestionCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestionCategory/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Enabled,Created_date")] QuestionCategoryModels questionCategoryModels)
        {
            if (ModelState.IsValid)
            {
                db.QuestionCategoryModel.Add(questionCategoryModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionCategoryModels);
        }

        // GET: QuestionCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionCategoryModels questionCategoryModels = db.QuestionCategoryModel.Find(id);
            if (questionCategoryModels == null)
            {
                return HttpNotFound();
            }
            return View(questionCategoryModels);
        }

        // POST: QuestionCategory/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Enabled,Created_date")] QuestionCategoryModels questionCategoryModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionCategoryModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionCategoryModels);
        }

        // GET: QuestionCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionCategoryModels questionCategoryModels = db.QuestionCategoryModel.Find(id);
            if (questionCategoryModels == null)
            {
                return HttpNotFound();
            }
            return View(questionCategoryModels);
        }

        // POST: QuestionCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionCategoryModels questionCategoryModels = db.QuestionCategoryModel.Find(id);
            db.QuestionCategoryModel.Remove(questionCategoryModels);
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
