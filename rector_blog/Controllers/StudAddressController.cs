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
    public class StudAddressController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudAddress
        public ActionResult Index()
        {
            var studAddressModels = db.StudAddressModel.Include(s => s.StudModels);
            return View(studAddressModels.ToList());
        }

        // GET: StudAddress/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudAddressModels studAddressModels = db.StudAddressModel.Find(id);
            if (studAddressModels == null)
            {
                return HttpNotFound();
            }
            return View(studAddressModels);
        }

        // GET: StudAddress/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.StudModel, "ID", "Name");
            return View();
        }

        // POST: StudAddress/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Address")] StudAddressModels studAddressModels)
        {
            if (ModelState.IsValid)
            {
                db.StudAddressModel.Add(studAddressModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.StudModel, "ID", "Name", studAddressModels.ID);
            return View(studAddressModels);
        }

        // GET: StudAddress/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudAddressModels studAddressModels = db.StudAddressModel.Find(id);
            if (studAddressModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.StudModel, "ID", "Name", studAddressModels.ID);
            return View(studAddressModels);
        }

        // POST: StudAddress/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Address")] StudAddressModels studAddressModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studAddressModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.StudModel, "ID", "Name", studAddressModels.ID);
            return View(studAddressModels);
        }

        // GET: StudAddress/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudAddressModels studAddressModels = db.StudAddressModel.Find(id);
            if (studAddressModels == null)
            {
                return HttpNotFound();
            }
            return View(studAddressModels);
        }

        // POST: StudAddress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudAddressModels studAddressModels = db.StudAddressModel.Find(id);
            db.StudAddressModel.Remove(studAddressModels);
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
