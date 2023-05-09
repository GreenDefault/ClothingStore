using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();
        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAd(string user_name, string pass_word)
        {
            if (user_name != null)
            {
                var usr = db.TaiKhoans
            .Where(m => m.TenDN.Equals(user_name))
            .Where(m => m.MatKhau.Equals(pass_word))
            .FirstOrDefault();
                if (usr != null && usr.MaCV == 1)
                {
                    Session["user_name_admin"] = usr.MaTK;
                    return RedirectToAction("Index", "HoaDons");
                }
            }
            return View("Fail");
        }
        public ActionResult LogoutAd()
        {
            Session["user_name_admin"] = null;
            return RedirectToAction("Index");
        }
    }
}