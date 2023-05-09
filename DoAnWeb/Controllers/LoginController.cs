using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.Controllers
{
    public class LoginController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();
        // GET: Login
        public ActionResult Index(string returnurl)
        {
            return View((object)returnurl);
        }

        public ActionResult Fail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string user_name, string pass_word, string returnurl)
        {
            if (user_name != null)
            {
                var usr = db.TaiKhoans
            .Where(m => m.TenDN.Equals(user_name))
            .Where(m => m.MatKhau.Equals(pass_word))
            .FirstOrDefault();
                if (usr != null)
                {
                    Session["tennd"] = usr.TenDN;
                    Session["user_name"] = usr.MaTK;
                    if(!string.IsNullOrEmpty(returnurl))
                    {
                        return Redirect(returnurl);
                    }
                    return RedirectToAction("Index", "AnSPnSL");
                }
                else
                {
                    TempData["error"]= "<p class=\"error-login\">Sai tài khoản hoặc mật khẩu!</p>";
                    return View("Index");
                }
            }
            return View("Index");
        }
        public ActionResult LogOut()
        {
            Session["user_name"] = null;
            Session["tennd"] = null;
            Session["Cart"] = null;
            return RedirectToAction("Index");
        }
    }
}