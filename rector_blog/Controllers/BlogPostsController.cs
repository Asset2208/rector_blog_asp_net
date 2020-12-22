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
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        //public ActionResult Index()
        //{
        //    var blogPostModel = db.BlogPostModel.Include(b => b.BlogCategoryModels);
        //    return View(blogPostModel.ToList());
        //}
        public ActionResult Index(int page = 1)
        {
            var blogPostModel = db.BlogPostModel.Include(b => b.BlogCategoryModels).OrderByDescending(b => b.Created_date).ToList();

            int pageSize = 4; // количество объектов на страницу
            IEnumerable<BlogPostsModels> blogPostsPerPages = blogPostModel.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = blogPostModel.Count() };
            BlogPageViewModel ivm = new BlogPageViewModel { PageInfo = pageInfo, BlogPostsModels = blogPostsPerPages };
            return View(ivm);
        }

        public ActionResult Search(string search)
        {
            var blogPostModel = db.BlogPostModel.Include(b => b.BlogCategoryModels).Where(a => a.Title.Contains(search)).ToList();
            ViewBag.Posts = blogPostModel;
            return View("Search");
        }

        public ActionResult AutocompleteSearch(string term)
        {

            var models = db.BlogPostModel.Include(b => b.BlogCategoryModels).Where(a => a.Title.Contains(term))
                            .Select(a => new { value = a.Title })
                            .Distinct();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostsModels blogPostsModels = db.BlogPostModel.Include(c => c.Comments).Include(c => c.BlogCategoryModels).FirstOrDefault(c => c.Id == id);
            //BlogPostsModels blogPostsModels = db.BlogPostModel.Find(id);
            if (blogPostsModels == null)
            {
                return HttpNotFound();
            }
            blogPostsModels.Views = blogPostsModels.Views + 1;
            db.Entry(blogPostsModels).State = EntityState.Modified;
            db.SaveChanges();

            return View(blogPostsModels);
        }

        [HttpPost]
        public JsonResult IsURLExist(string ImageUrl)
        {
            return Json(IsURLExistbyURL(ImageUrl));
        }

        
        public bool IsURLExistbyURL(string ImgUrl)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ImgUrl);
                req.AllowAutoRedirect = false;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                if (res.StatusCode == HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
            catch(Exception ed)
            {
                return false;
            }
            
        }

        // GET: BlogPosts/Create
        [CustomAuth(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.BlogCategoryModelsId = new SelectList(db.BlogCategoryModel, "ID", "Name");
            return PartialView();
        }

        // POST: BlogPosts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Created_date,Title,Content,ImageUrl,Views,Enabled,Comments_enabled,BlogCategoryModelsId")] BlogPostsModels blogPostsModels)
        {
            if (ModelState.IsValid)
            {
                db.BlogPostModel.Add(blogPostsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogCategoryModelsId = new SelectList(db.BlogCategoryModel, "ID", "Name", blogPostsModels.BlogCategoryModelsId);
            return View(blogPostsModels);
        }

        // GET: BlogPosts/Edit/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostsModels blogPostsModels = db.BlogPostModel.Find(id);
            if (blogPostsModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlogCategoryModelsId = new SelectList(db.BlogCategoryModel, "ID", "Name", blogPostsModels.BlogCategoryModelsId);
            return View(blogPostsModels);
        }

        // POST: BlogPosts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomAuth(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created_date,Title,Content,ImageUrl,Views,Enabled,Comments_enabled,BlogCategoryModelsId")] BlogPostsModels blogPostsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogPostsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogCategoryModelsId = new SelectList(db.BlogCategoryModel, "ID", "Name", blogPostsModels.BlogCategoryModelsId);
            return View(blogPostsModels);
        }

        // GET: BlogPosts/Delete/5
        [CustomAuth(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPostsModels blogPostsModels = db.BlogPostModel.Find(id);
            if (blogPostsModels == null)
            {
                return HttpNotFound();
            }
            return View(blogPostsModels);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPostsModels blogPostsModels = db.BlogPostModel.Find(id);
            db.BlogPostModel.Remove(blogPostsModels);
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
