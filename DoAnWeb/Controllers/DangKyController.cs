using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.Controllers
{
    public class DangKyController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();
        // GET: DangKi
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XacNhan(FormCollection forms)
        {
            var tk = db.TaiKhoans.Where(m => m.TenDN.Equals(forms["TenDN"].ToString()));
            if(tk!=null)
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.TenNguoiDung = forms["Ten"];
                taiKhoan.SDT = forms["SDT"];
                taiKhoan.MatKhau = forms["MatKhau"];
                taiKhoan.TenDN = forms["TenDN"];
                taiKhoan.Email = forms["email"];
                taiKhoan.DiaChi = forms["diachi"];
                taiKhoan.MaCV = 2;
                taiKhoan.Anh = "user1.jpg";
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Login");
        }
    }
}