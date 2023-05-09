using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class GioHangsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/GioHangs
        public ActionResult Index()
        {
            var gioHangs = db.GioHangs.Include(g => g.ChatLieu).Include(g => g.Mau).Include(g => g.SanPham).Include(g => g.Size).Include(g => g.TaiKhoan);
            return View(gioHangs.ToList());
        }

        // GET: Admin/GioHangs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // GET: Admin/GioHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu");
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize");
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN");
            return View();
        }

        // POST: Admin/GioHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTK,MaSP,MaCL,MaSize,MaMau,SoLuong,ThanhTien")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", gioHang.MaCL);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", gioHang.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", gioHang.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", gioHang.MaSize);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", gioHang.MaTK);
            return View(gioHang);
        }

        // GET: Admin/GioHangs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", gioHang.MaCL);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", gioHang.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", gioHang.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", gioHang.MaSize);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", gioHang.MaTK);
            return View(gioHang);
        }

        // POST: Admin/GioHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTK,MaSP,MaCL,MaSize,MaMau,SoLuong,ThanhTien")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", gioHang.MaCL);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", gioHang.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", gioHang.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", gioHang.MaSize);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", gioHang.MaTK);
            return View(gioHang);
        }

        // GET: Admin/GioHangs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // POST: Admin/GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            GioHang gioHang = db.GioHangs.Find(id);
            db.GioHangs.Remove(gioHang);
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
