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
    public class AnhSPsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/AnhSPs
        public ActionResult Index()
        {
            var anhSPs = db.AnhSPs.Include(a => a.Mau).Include(a => a.SanPham);
            return View(anhSPs.ToList());
        }

        // GET: Admin/AnhSPs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnhSP anhSP = db.AnhSPs.Find(id);
            if (anhSP == null)
            {
                return HttpNotFound();
            }
            return View(anhSP);
        }

        // GET: Admin/AnhSPs/Create
        public ActionResult Create()
        {
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: Admin/AnhSPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaMau,Anh")] AnhSP anhSP)
        {
            if (ModelState.IsValid)
            {
                db.AnhSPs.Add(anhSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", anhSP.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", anhSP.MaSP);
            return View(anhSP);
        }

        // GET: Admin/AnhSPs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnhSP anhSP = db.AnhSPs.Find(id);
            if (anhSP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", anhSP.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", anhSP.MaSP);
            return View(anhSP);
        }

        // POST: Admin/AnhSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaMau,Anh")] AnhSP anhSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anhSP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", anhSP.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", anhSP.MaSP);
            return View(anhSP);
        }

        // GET: Admin/AnhSPs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnhSP anhSP = db.AnhSPs.Find(id);
            if (anhSP == null)
            {
                return HttpNotFound();
            }
            return View(anhSP);
        }

        // POST: Admin/AnhSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AnhSP anhSP = db.AnhSPs.Find(id);
            db.AnhSPs.Remove(anhSP);
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
