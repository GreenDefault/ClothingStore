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
    public class CommentsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.SanPham).Include(c => c.TaiKhoan);
            return View(comments.ToList());
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Admin/Comments/Create
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN");
            return View();
        }

        // POST: Admin/Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCMT,MaTK,MaSP,DanhGia,CMT,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", comment.MaSP);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", comment.MaTK);
            return View(comment);
        }

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", comment.MaSP);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", comment.MaTK);
            return View(comment);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCMT,MaTK,MaSP,DanhGia,CMT,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", comment.MaSP);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", comment.MaTK);
            return View(comment);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Comment comment = db.Comments.Find(id);
            var repcmt = db.RepCMTs.Where(m => m.MaCMT == id).ToList();
            foreach (var item in repcmt)
            {
                db.RepCMTs.Remove(item);
            }
            db.Comments.Remove(comment);
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
