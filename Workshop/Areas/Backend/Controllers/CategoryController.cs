using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workshop.Helpers;
using Workshop.Models;
using Workshop.ViewModels;

namespace Workshop.Areas.Backend.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private WorkshopEntities db = new WorkshopEntities();

        public ActionResult Index(QueryOption<Category> queryOption)
        {
            var query = db.Category.AsQueryable();

            if (!string.IsNullOrEmpty(queryOption.Keyword))
            {
                query = query.Where(a => a.Name.Contains(queryOption.Keyword));
            }

            queryOption.SetSource(query);

            return View(queryOption);
        }

        public ActionResult Details(Guid id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (db.Category.Any(x => x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "文章分類名稱不可重複");
                return View(category);
            }

            if (ModelState.IsValid)
            {
                category.ID = Guid.NewGuid();
                category.CreateUser = WebSiteHelper.CurrentUserID;
                category.CreateDate = DateTime.Now;
                category.UpdateDate = DateTime.Now;

                db.Category.Add(category);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(Guid id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (db.Category.Any(x => x.ID != category.ID && x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "文章分類名稱不可重複");
                return View(category);
            }

            if (ModelState.IsValid)
            {
                var original = db.Category.FirstOrDefault(x => x.ID == category.ID);

                original.Name = category.Name;
                original.UpdateUser = WebSiteHelper.CurrentUserID;
                original.UpdateDate = DateTime.Now;

                db.Entry(original).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(Guid id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Category category = db.Category.Find(id);
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}