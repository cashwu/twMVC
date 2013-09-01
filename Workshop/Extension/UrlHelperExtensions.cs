using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workshop.Models;

namespace Workshop.Helpers
{
    public static class UrlHelperExtensions
    {
        public static string CategoryUrl(this UrlHelper urlHelper, Category category)
        {
            var categoryName = category.Name; //.ToUrlFriendly();

            return urlHelper.Action("Index", "Article", new
            {
                area = "", 
                categoryName
            });
        }

        public static string ArticleUrl(this UrlHelper urlHelper, Article article)
        {
            var date = article.PublishDate.ToString("yyyy-MM-dd");

            var categoryName = article.Category.Name; //.ToUrlFriendly();

            var subject = article.Subject; //.ToUrlFriendly();

            return urlHelper.Action("SeoDetails", "Article", new
            {
                area = "", 
                article.PublishDate.Year, 
                article.PublishDate.Month, 
                article.PublishDate.Day, 
                categoryName, 
                subject
            });
        }
    }
}