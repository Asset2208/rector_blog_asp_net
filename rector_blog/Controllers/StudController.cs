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
    public class StudController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stud
        public ActionResult Index()
        {
            var studModel = db.StudModel.Include(s => s.StudAddressModels);
            return View(studModel.ToList());
        }

        // GET: Stud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudModels studModels = db.StudModel.Find(id);
            if (studModels == null)
            {
                return HttpNotFound();
            }
            return View(studModels);
        }

        // GET: Stud/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.StudAddressModel, "ID", "Address");
            return View();
        }

        // POST: Stud/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] StudModels studModels)
        {
            if (ModelState.IsValid)
            {
                db.StudModel.Add(studModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.StudAddressModel, "ID", "Address", studModels.ID);
            return View(studModels);
        }

        // GET: Stud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudModels studModels = db.StudModel.Find(id);
            if (studModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.StudAddressModel, "ID", "Address", studModels.ID);
            return View(studModels);
        }

        // POST: Stud/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] StudModels studModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.StudAddressModel, "ID", "Address", studModels.ID);
            return View(studModels);
        }

        // GET: Stud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudModels studModels = db.StudModel.Find(id);
            if (studModels == null)
            {
                return HttpNotFound();
            }
            return View(studModels);
        }

        // POST: Stud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudModels studModels = db.StudModel.Find(id);
            db.StudModel.Remove(studModels);
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
