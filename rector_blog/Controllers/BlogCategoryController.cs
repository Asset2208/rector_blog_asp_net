using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using rector_blog.Models;
using rector_blog.Filters;

namespace rector_blog.Controllers
{
    public class BlogCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogCategory
        [CustomAuth(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.BlogCategoryModel.ToList());
        }

        // GET: BlogCategory/Details/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategoryModels blogCategoryModels = db.BlogCategoryModel.Find(id);
            if (blogCategoryModels == null)
            {
                return HttpNotFound();
            }
            return View(blogCategoryModels);
        }

        // GET: BlogCategory/Create
        [CustomAuth(Roles = "Admin")]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: BlogCategory/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Enabled,Created_date")] BlogCategoryModels blogCategoryModels)
        {
            if (ModelState.IsValid)
            {
                db.BlogCategoryModel.Add(blogCategoryModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogCategoryModels);
        }



        // GET: BlogCategory/Edit/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategoryModels blogCategoryModels = db.BlogCategoryModel.Find(id);
            if (blogCategoryModels == null)
            {
                return HttpNotFound();
            }
            return View(blogCategoryModels);
        }

        // POST: BlogCategory/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Enabled,Created_date")] BlogCategoryModels blogCategoryModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogCategoryModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogCategoryModels);
        }

        // GET: BlogCategory/Delete/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategoryModels blogCategoryModels = db.BlogCategoryModel.Find(id);
            if (blogCategoryModels == null)
            {
                return HttpNotFound();
            }
            return View(blogCategoryModels);
        }

        // POST: BlogCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogCategoryModels blogCategoryModels = db.BlogCategoryModel.Find(id);
            db.BlogCategoryModel.Remove(blogCategoryModels);
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
