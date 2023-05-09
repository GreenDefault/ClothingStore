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
    public class RepCMTsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/RepCMTs
        public ActionResult Index()
        {
            var repCMTs = db.RepCMTs.Include(r => r.Comment).Include(r => r.TaiKhoan);
            return View(repCMTs.ToList());
        }

        // GET: Admin/RepCMTs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepCMT repCMT = db.RepCMTs.Find(id);
            if (repCMT == null)
            {
                return HttpNotFound();
            }
            return View(repCMT);
        }

        // GET: Admin/RepCMTs/Create
        public ActionResult Create()
        {
            ViewBag.MaCMT = new SelectList(db.Comments, "MaCMT", "CMT");
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN");
            return View();
        }

        // POST: Admin/RepCMTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaRep,MaCMT,MaTK,NoiDung,Date")] RepCMT repCMT)
        {
            if (ModelState.IsValid)
            {
                db.RepCMTs.Add(repCMT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCMT = new SelectList(db.Comments, "MaCMT", "CMT", repCMT.MaCMT);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", repCMT.MaTK);
            return View(repCMT);
        }

        // GET: Admin/RepCMTs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepCMT repCMT = db.RepCMTs.Find(id);
            if (repCMT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCMT = new SelectList(db.Comments, "MaCMT", "CMT", repCMT.MaCMT);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", repCMT.MaTK);
            return View(repCMT);
        }

        // POST: Admin/RepCMTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaRep,MaCMT,MaTK,NoiDung,Date")] RepCMT repCMT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repCMT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCMT = new SelectList(db.Comments, "MaCMT", "CMT", repCMT.MaCMT);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", repCMT.MaTK);
            return View(repCMT);
        }

        // GET: Admin/RepCMTs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepCMT repCMT = db.RepCMTs.Find(id);
            if (repCMT == null)
            {
                return HttpNotFound();
            }
            return View(repCMT);
        }

        // POST: Admin/RepCMTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RepCMT repCMT = db.RepCMTs.Find(id);
            db.RepCMTs.Remove(repCMT);
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
