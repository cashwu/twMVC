using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workshop.Models;

namespace Workshop.Controllers
{
    public class HomeController : Controller
    {
        private WorkshopEntities db = new WorkshopEntities();

        public ActionResult Index()
        {
            //測試 ddl
            var Categories = db.Category.OrderBy(a => a.CreateDate);

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var c in Categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.ID.ToString(),
                    Selected = c.ID.ToString().Equals("67982c0a-f672-4e23-85d8-275595456ccb")
                });
            }

            ViewBag.ArticleCategories = items;



            //最新五篇文章
            var articlesList = db.Article.Where(x => x.IsPublish && x.PublishDate <= DateTime.Now)
                .OrderByDescending(x => x.CreateDate)
                .Take(5).ToList();

            return View(articlesList);
        }
    }
}
