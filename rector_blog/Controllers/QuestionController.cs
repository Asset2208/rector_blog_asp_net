using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using rector_blog.Models;

namespace rector_blog.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Question
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var questionModel = db.QuestionModel.Include(q => q.QuestionBlogPostModels);
            return View(questionModel.ToList());
        }

        // GET: Question/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModels questionModels = db.QuestionModel.Find(id);
            if (questionModels == null)
            {
                return HttpNotFound();
            }
            return View(questionModels);
        }

        // GET: Question/Create
        [Authorize(Roles = "Admin,User")]
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.QuestionBlogPostModel, "ID", "Answer");
            return PartialView();
        }

        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }

        // POST: Question/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,User_id,Is_answered,Created_date")] QuestionModels questionModels)
        {
            CaptchaResponse response = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (response.Success && ModelState.IsValid)
            {
                questionModels.ApplicationUser = db.Users.Find(questionModels.User_id);
                db.QuestionModel.Add(questionModels);
                db.SaveChanges();
                return RedirectToAction("Index", "QuestionBlogPost");
            }
            else
            {
                return Content("Error From Google ReCaptcha : " + response.ErrorMessage[0].ToString());
            }

            //ViewBag.ID = new SelectList(db.QuestionBlogPostModel, "ID", "Answer", questionModels.ID);
            //return View(questionModels);
        }

        // GET: Question/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModels questionModels = db.QuestionModel.Find(id);
            if (questionModels == null)
            {
                return HttpNotFound();
            }

            ViewBag.User_id = questionModels.User_id;
            ViewBag.ID = new SelectList(db.QuestionBlogPostModel, "ID", "Answer", questionModels.ID);
            return View(questionModels);
        }

        // POST: Question/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,User_id,Is_answered,Created_date")] QuestionModels questionModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.QuestionBlogPostModel, "ID", "Answer", questionModels.ID);
            return View(questionModels);
        }

        // GET: Question/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionModels questionModels = db.QuestionModel.Find(id);
            if (questionModels == null)
            {
                return HttpNotFound();
            }
            return View(questionModels);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionModels questionModels = db.QuestionModel.Find(id);
            db.QuestionModel.Remove(questionModels);
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
