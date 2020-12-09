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
    public class TestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Test
        public ActionResult Index()
        {
            return View(db.TestModel.ToList());
        }

        // GET: Test/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestModels testModels = db.TestModel.Find(id);
            if (testModels == null)
            {
                return HttpNotFound();
            }
            return View(testModels);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Age")] TestModels testModels)
        {
            if (ModelState.IsValid)
            {
                db.TestModel.Add(testModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testModels);
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestModels testModels = db.TestModel.Find(id);
            if (testModels == null)
            {
                return HttpNotFound();
            }
            return View(testModels);
        }

        // POST: Test/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Age")] TestModels testModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testModels);
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestModels testModels = db.TestModel.Find(id);
            if (testModels == null)
            {
                return HttpNotFound();
            }
            return View(testModels);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestModels testModels = db.TestModel.Find(id);
            db.TestModel.Remove(testModels);
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
