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
    public class ThuongHieuxController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/ThuongHieux
        public ActionResult Index()
        {
            return View(db.ThuongHieux.ToList());
        }

        // GET: Admin/ThuongHieux/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ThuongHieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTH,TenTH")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                db.ThuongHieux.Add(thuongHieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieux/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTH,TenTH")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thuongHieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thuongHieu);
        }

        // GET: Admin/ThuongHieux/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: Admin/ThuongHieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ThuongHieu thuongHieu = db.ThuongHieux.Find(id);
            db.ThuongHieux.Remove(thuongHieu);
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
