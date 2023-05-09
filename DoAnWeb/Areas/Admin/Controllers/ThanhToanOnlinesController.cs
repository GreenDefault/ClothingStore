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
    public class ThanhToanOnlinesController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/ThanhToanOnlines
        public ActionResult Index()
        {
            return View(db.ThanhToanOnlines.ToList());
        }

        // GET: Admin/ThanhToanOnlines/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhToanOnline thanhToanOnline = db.ThanhToanOnlines.Find(id);
            if (thanhToanOnline == null)
            {
                return HttpNotFound();
            }
            return View(thanhToanOnline);
        }

        // GET: Admin/ThanhToanOnlines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThanhToanOnlines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThe,TenThe,Anh")] ThanhToanOnline thanhToanOnline)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                HttpPostedFileBase Anh = Request.Files["Anh"];
                if (Anh != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                    Anh.SaveAs(path);
                    thanhToanOnline.Anh = Anh.FileName;
                }
                db.ThanhToanOnlines.Add(thanhToanOnline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thanhToanOnline);
        }

        // GET: Admin/ThanhToanOnlines/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhToanOnline thanhToanOnline = db.ThanhToanOnlines.Find(id);
            if (thanhToanOnline == null)
            {
                return HttpNotFound();
            }
            return View(thanhToanOnline);
        }

        // POST: Admin/ThanhToanOnlines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThe,TenThe,Anh")] ThanhToanOnline thanhToanOnline)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase Anh = Request.Files["Anh"];
                if (Anh != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                    Anh.SaveAs(path);
                    thanhToanOnline.Anh = Anh.FileName;
                }
                db.ThanhToanOnlines.Add(thanhToanOnline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thanhToanOnline);
        }

        // GET: Admin/ThanhToanOnlines/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhToanOnline thanhToanOnline = db.ThanhToanOnlines.Find(id);
            if (thanhToanOnline == null)
            {
                return HttpNotFound();
            }
            return View(thanhToanOnline);
        }

        // POST: Admin/ThanhToanOnlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ThanhToanOnline thanhToanOnline = db.ThanhToanOnlines.Find(id);
            db.ThanhToanOnlines.Remove(thanhToanOnline);
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
