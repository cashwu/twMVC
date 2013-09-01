using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Workshop.Areas.Backend.Models;
using Workshop.Helpers;
using Workshop.Models;

namespace Workshop.Areas.Backend.Controllers
{
    [Authorize]
    public class SystemUserController : Controller
    {
        private WorkshopEntities db = new WorkshopEntities();

        public ActionResult Index()
        {
            return View(db.SystemUser.ToList());
        }

        public ActionResult Details(Guid id)
        {
            SystemUser systemuser = db.SystemUser.Find(id);
            if (systemuser == null)
            {
                return HttpNotFound();
            }
            return View(systemuser);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SystemUser systemuser)
        {
            //檢查登入帳號是否重複
            if (db.SystemUser.Any(x => x.Account == systemuser.Account))
            {
                ModelState.AddModelError("Account", "登入帳號不可重複");
                return View(systemuser);
            }

            if (ModelState.IsValid)
            {
                systemuser.ID = Guid.NewGuid();

                systemuser.Salt = GenerateSalt();
                systemuser.Password = CryptographyPassword(systemuser.Password, systemuser.Salt);

                systemuser.CreateUser = WebSiteHelper.CurrentUserID;
                systemuser.CreateDate = DateTime.Now;
                systemuser.UpdateDate = DateTime.Now;

                db.SystemUser.Add(systemuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemuser);
        }

        public ActionResult Edit(Guid id)
        {
            SystemUser systemuser = db.SystemUser.Find(id);
            if (systemuser == null)
            {
                return HttpNotFound();
            }
            return View(systemuser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SystemUser systemuser)
        {
            //檢查登入帳號是否重複
            if (db.SystemUser.Any(x => x.ID != systemuser.ID && x.Account == systemuser.Account))
            {
                ModelState.AddModelError("Account", "登入帳號不可重複");
                return View(systemuser);
            }

            if (ModelState.IsValid)
            {
                SystemUser user = db.SystemUser.FirstOrDefault(x => x.ID == systemuser.ID);

                if (!string.IsNullOrWhiteSpace(systemuser.Password))
                {
                    user.Salt = GenerateSalt();
                    user.Password = CryptographyPassword(systemuser.Password, systemuser.Salt);
                }

                user.Name = systemuser.Name;
                user.Account = systemuser.Account;
                user.Email = systemuser.Email;

                user.UpdateUser = WebSiteHelper.CurrentUserID;
                user.UpdateDate = DateTime.Now;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(systemuser);
        }

        public ActionResult Delete(Guid id)
        {
            SystemUser systemuser = db.SystemUser.Find(id);
            if (systemuser == null)
            {
                return HttpNotFound();
            }
            return View(systemuser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SystemUser systemuser = db.SystemUser.Find(id);
            db.SystemUser.Remove(systemuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private string GenerateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buf = new byte[16];
            rng.GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        private string CryptographyPassword(string password, string salt)
        {
            string cryptographyPassword =
                FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, "sha1");

            return cryptographyPassword;
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [AllowAnonymous]
        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(HttpAntiForgeryException),View = "ErrorCC")]
        public ActionResult Logon(LogonViewModel logonModel)
        {
            if (ModelState.IsValid)
            {
                var systemuser = db.SystemUser.FirstOrDefault(x => x.Account == logonModel.Account);

                if (systemuser == null)
                {
                    ModelState.AddModelError("", "請輸入正確的帳號或密碼!");
                }
                else
                {
                    var password = CryptographyPassword(logonModel.Password, systemuser.Salt);

                    if (systemuser.Password == password)
                    {
                        var now = DateTime.Now;

                        var ticket = new FormsAuthenticationTicket(
                                         1,
                                         systemuser.Name,
                                         now,
                                         now.AddMinutes(30),
                                         logonModel.Remember,
                                         systemuser.ID.ToString(),
                                         FormsAuthentication.FormsCookiePath);

                        var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        Response.Cookies.Add(cookie);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "請輸入正確的帳號或密碼!");
                    }
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Logon", "SystemUser");
        }


    }
}