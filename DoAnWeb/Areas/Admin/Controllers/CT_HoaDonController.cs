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
    public class CT_HoaDonController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/CT_HoaDon
        public ActionResult Index()
        {
            var cT_HoaDon = db.CT_HoaDon.Include(c => c.ChatLieu).Include(c => c.HoaDon).Include(c => c.Mau).Include(c => c.SanPham).Include(c => c.Size);
            return View(cT_HoaDon.ToList());
        }

        // GET: Admin/CT_HoaDon/Details/5
        public ActionResult Details(long? id, long? di)
        {
            if (id == null || di == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_HoaDon cT_HoaDon = db.CT_HoaDon.Find(id, di);
            if (cT_HoaDon == null)
            {
                return HttpNotFound();
            }
            return View(cT_HoaDon);
        }

        // GET: Admin/CT_HoaDon/Create
        public ActionResult Create()
        {
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu");
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "DiaChi");
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize");
            return View();
        }

        // POST: Admin/CT_HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaSP,MaCL,MaSize,MaMau,SoLuong,ThanhTien")] CT_HoaDon cT_HoaDon)
        {
            if (ModelState.IsValid)
            {
                db.CT_HoaDon.Add(cT_HoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", cT_HoaDon.MaCL);
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "DiaChi", cT_HoaDon.MaHD);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", cT_HoaDon.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cT_HoaDon.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", cT_HoaDon.MaSize);
            return View(cT_HoaDon);
        }

        // GET: Admin/CT_HoaDon/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_HoaDon cT_HoaDon = db.CT_HoaDon.Find(id);
            if (cT_HoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", cT_HoaDon.MaCL);
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "DiaChi", cT_HoaDon.MaHD);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", cT_HoaDon.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cT_HoaDon.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", cT_HoaDon.MaSize);
            return View(cT_HoaDon);
        }

        // POST: Admin/CT_HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaSP,MaCL,MaSize,MaMau,SoLuong,ThanhTien")] CT_HoaDon cT_HoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_HoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", cT_HoaDon.MaCL);
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "DiaChi", cT_HoaDon.MaHD);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", cT_HoaDon.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", cT_HoaDon.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", cT_HoaDon.MaSize);
            return View(cT_HoaDon);
        }

        // GET: Admin/CT_HoaDon/Delete/5
        public ActionResult Delete(long? id, long? di)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_HoaDon cT_HoaDon = db.CT_HoaDon.Find(id, di);
            if (cT_HoaDon == null)
            {
                return HttpNotFound();
            }
            return View(cT_HoaDon);
        }

        // POST: Admin/CT_HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id, long di)
        {
            CT_HoaDon cT_HoaDon = db.CT_HoaDon.Find(id, di);
            db.CT_HoaDon.Remove(cT_HoaDon);
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

        public ActionResult DetailsBill(long? acc)
        {
            var a = db.CT_HoaDon.Where(m => m.MaHD == acc);
            return View("Index", a);
        }
    }
}
