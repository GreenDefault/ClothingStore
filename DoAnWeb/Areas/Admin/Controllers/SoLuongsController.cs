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
    public class SoLuongsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/SoLuongs
        public ActionResult Index()
        {
            var soLuongs = db.SoLuongs.Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham);
            return View(soLuongs.ToList());
        }

        // GET: Admin/SoLuongs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuong soLuong = db.SoLuongs.Find(id);
            if (soLuong == null)
            {
                return HttpNotFound();
            }
            return View(soLuong);
        }

        // GET: Admin/SoLuongs/Create
        public ActionResult Create()
        {
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau");
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/SoLuongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaMau,MaSize,SoLuong1")] SoLuong soLuong)
        {
            if (ModelState.IsValid)
            {
                db.SoLuongs.Add(soLuong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", soLuong.MaMau);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", soLuong.MaSize);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", soLuong.MaSP);
            return View(soLuong);
        }

        // GET: Admin/SoLuongs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuong soLuong = db.SoLuongs.Find(id);
            if (soLuong == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", soLuong.MaMau);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", soLuong.MaSize);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", soLuong.MaSP);
            return View(soLuong);
        }

        // POST: Admin/SoLuongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaMau,MaSize,SoLuong1")] SoLuong soLuong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soLuong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", soLuong.MaMau);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", soLuong.MaSize);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", soLuong.MaSP);
            return View(soLuong);
        }

        // GET: Admin/SoLuongs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoLuong soLuong = db.SoLuongs.Find(id);
            if (soLuong == null)
            {
                return HttpNotFound();
            }
            return View(soLuong);
        }

        // POST: Admin/SoLuongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SoLuong soLuong = db.SoLuongs.Find(id);
            db.SoLuongs.Remove(soLuong);
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
