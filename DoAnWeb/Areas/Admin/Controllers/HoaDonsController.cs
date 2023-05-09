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
    public class HoaDonsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/HoaDons
        public ActionResult Index()
        {
            var hoaDons = db.HoaDons.Include(h => h.TaiKhoan).Include(h => h.ThanhToanOnline).Include(h => h.TrangThai1);
            return View(hoaDons.ToList());
        }

        // GET: Admin/HoaDons/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN");
            ViewBag.MaThe = new SelectList(db.ThanhToanOnlines, "MaThe", "TenThe");
            ViewBag.TrangThai = new SelectList(db.TrangThais, "MaTrangThai", "TenTrangThai");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaTK,NgayMua,MaThe,DiaChi,TongTien,TrangThai,SDTKH")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", hoaDon.MaTK);
            ViewBag.MaThe = new SelectList(db.ThanhToanOnlines, "MaThe", "TenThe", hoaDon.MaThe);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "MaTrangThai", "TenTrangThai", hoaDon.TrangThai);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", hoaDon.MaTK);
            ViewBag.MaThe = new SelectList(db.ThanhToanOnlines, "MaThe", "TenThe", hoaDon.MaThe);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "MaTrangThai", "TenTrangThai", hoaDon.TrangThai);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaTK,NgayMua,MaThe,DiaChi,TongTien,TrangThai,SDTKH")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", hoaDon.MaTK);
            ViewBag.MaThe = new SelectList(db.ThanhToanOnlines, "MaThe", "TenThe", hoaDon.MaThe);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "MaTrangThai", "TenTrangThai", hoaDon.TrangThai);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
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
        public ActionResult TT(FormCollection form)
        {
            var a = DateTime.Parse(form["from"]);
            var b = DateTime.Parse(form["to"]);
            var c = db.HoaDons.Where(m => m.NgayMua >= a && m.NgayMua <= b).ToList();
            long l = 0;
            foreach (var k in c)
            {
                l += long.Parse(k.TongTien.ToString());
            }
            ViewBag.Tong = l;
            return View("Index", c);
        }
    }
}
