using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using rector_blog.Models;
using System.Net.Mail;

namespace rector_blog.Controllers
{
    public class QuestionBlogPostController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuestionBlogPost
        //public ActionResult Index()
        //{
        //    var questionBlogPostModels = db.QuestionBlogPostModel.Include(q => q.QuestionModels);
        //    return View(questionBlogPostModels.OrderByDescending(b => b.Created_date).ToList());
        //}

        public ActionResult Index(int page = 1)
        {
            var questionBlogPostModels = db.QuestionBlogPostModel.Include(q => q.QuestionModels).OrderByDescending(b => b.Created_date).ToList();

            int pageSize = 3; // количество объектов на страницу
            IEnumerable<QuestionBlogPostModels> questionPostsPerPages = questionBlogPostModels.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = questionBlogPostModels.Count() };
            PageViewModel ivm = new PageViewModel { PageInfo = pageInfo, QuestionBlogPostModels = questionPostsPerPages };
            return View(ivm);
        }

        // GET: QuestionBlogPost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //BlogPostsModels blogPostsModels = db.BlogPostModel.Include(c => c.Comments).Include(c => c.BlogCategoryModels).FirstOrDefault(c => c.Id == id);
            QuestionBlogPostModels questionBlogPostModels = db.QuestionBlogPostModel.Include(q => q.QuestionCategories).FirstOrDefault(q => q.ID == id);
            if (questionBlogPostModels == null)
            {
                return HttpNotFound();
            }

            questionBlogPostModels.Views = questionBlogPostModels.Views + 1;
            db.Entry(questionBlogPostModels).State = EntityState.Modified;
            db.SaveChanges();

            var user = db.Users.Find(questionBlogPostModels.QuestionModels.User_id);
            ViewBag.User = user;
            return View(questionBlogPostModels);
        }

        // GET: QuestionBlogPost/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.QuestionCategoryModel.ToList();
            ViewBag.ID = new SelectList(db.QuestionModel.Where(q => q.Is_answered == false), "ID", "Content");
            return View("Create");
        }

        // POST: QuestionBlogPost/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Views,Created_date,Answer")] QuestionBlogPostModels questionBlogPostModels, int[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                questionBlogPostModels.QuestionCategories.Clear();
                if (selectedCourses != null)
                {
                    //получаем выбранные курсы
                    foreach (var c in db.QuestionCategoryModel.Where(co => selectedCourses.Contains(co.ID)))
                    {
                        questionBlogPostModels.QuestionCategories.Add(c);
                    }
                }

                db.QuestionBlogPostModel.Add(questionBlogPostModels);
                
                QuestionModels questionModels = db.QuestionModel.Find(questionBlogPostModels.ID);
                questionModels.Is_answered = true;
                db.Entry(questionModels).State = EntityState.Modified;
                db.SaveChanges();

                await SendEmail(questionBlogPostModels);

                return RedirectToAction("Index");
            }
            

            ViewBag.ID = new SelectList(db.QuestionModel, "ID", "Title", questionBlogPostModels.ID);
            return View(questionBlogPostModels);
        }

        public async Task SendEmail(QuestionBlogPostModels questionBlogPostModels)
        {
          
            var user = db.Users.Find(questionBlogPostModels.QuestionModels.User_id);
            var body = "<p>Email From: {0} ({1})</p><p>Сообщение:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email));  // replace with valid value 
            message.From = new MailAddress("ent2ubt.kz@gmail.com");  // replace with valid value
            message.Subject = "Rector's blog";
            string mess = String.Format("Здравствуйте, {0}! Мы ответили на ваш вопрос: <b>'{1}'</b>. Вы можете ознокомиться с нашим ответом на сайте! <br><br> С уважением, ректор!", user.UserName, questionBlogPostModels.QuestionModels.Content);
            message.Body = string.Format(body, "Rector's blog", "ent2ubt.kz@gmail.com", mess);
            message.IsBodyHtml = true;
            Console.WriteLine(user.Email);
            Console.WriteLine(message.Body);

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
            
            
        }

        // GET: QuestionBlogPost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionBlogPostModels questionBlogPostModels = db.QuestionBlogPostModel.Find(id);
            if (questionBlogPostModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.QuestionModel, "ID", "Title", questionBlogPostModels.ID);
            return View(questionBlogPostModels);
        }

        // POST: QuestionBlogPost/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Views,Created_date,Answer")] QuestionBlogPostModels questionBlogPostModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionBlogPostModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.QuestionModel, "ID", "Title", questionBlogPostModels.ID);
            return View(questionBlogPostModels);
        }

        // GET: QuestionBlogPost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionBlogPostModels questionBlogPostModels = db.QuestionBlogPostModel.Find(id);
            if (questionBlogPostModels == null)
            {
                return HttpNotFound();
            }
            return View(questionBlogPostModels);
        }

        // POST: QuestionBlogPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionBlogPostModels questionBlogPostModels = db.QuestionBlogPostModel.Find(id);
            db.QuestionBlogPostModel.Remove(questionBlogPostModels);
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
