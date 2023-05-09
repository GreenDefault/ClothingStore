using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class LoaiSPsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/LoaiSPs
        public ActionResult Index()
        {
            return View(db.LoaiSPs.ToList());
        }

        // GET: Admin/LoaiSPs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // GET: Admin/LoaiSPs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiSP,TenLoaiSP,Anh")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase Anh = Request.Files["Anh"];
                if (Anh != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                    Anh.SaveAs(path);
                    loaiSP.Anh = Anh.FileName;
                }
                db.LoaiSPs.Add(loaiSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSP);
        }

        // GET: Admin/LoaiSPs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // POST: Admin/LoaiSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiSP,TenLoaiSP,Anh")] LoaiSP loaiSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSP);
        }

        // GET: Admin/LoaiSPs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            if (loaiSP == null)
            {
                return HttpNotFound();
            }
            return View(loaiSP);
        }

        // POST: Admin/LoaiSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LoaiSP loaiSP = db.LoaiSPs.Find(id);
            List<SanPham> phams = db.SanPhams.Where(m => m.MaLoaiSP == id).ToList();
            foreach(var thing in phams)
            {
                List<SoLuong> soLuongs = db.SoLuongs.Where(m => m.MaSP.Equals(thing.MaSP)).ToList();
                List<AnhSP> anhSPs = db.AnhSPs.Where(m => m.MaSP.Equals(thing.MaSP)).ToList();
                List<CT_HoaDon> cT_HoaDons = db.CT_HoaDon.Where(m => m.MaSP == thing.MaSP).ToList();
                List<GioHang> gioHangs = db.GioHangs.Where(m => m.MaSP == thing.MaSP).ToList();
                List<Comment> comments = db.Comments.Where(m => m.MaSP == thing.MaSP).ToList();
                foreach (var item in cT_HoaDons)
                {
                    db.CT_HoaDon.Remove(item);
                }
                foreach (var item in gioHangs)
                {
                    db.GioHangs.Remove(item);
                }
                foreach (var item in soLuongs)
                {
                    db.SoLuongs.Remove(item);
                }
                foreach (var item in anhSPs)
                {
                    db.AnhSPs.Remove(item);
                }
                foreach (var item in comments)
                {
                    foreach (var temi in item.RepCMTs.ToList())
                    {
                        db.RepCMTs.Remove(temi);
                    }
                    db.Comments.Remove(item);
                }
                db.SanPhams.Remove(thing);
            }
            db.LoaiSPs.Remove(loaiSP);
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
