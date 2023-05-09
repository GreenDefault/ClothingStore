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
    public class TrangThaisController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/TrangThais
        public ActionResult Index()
        {
            return View(db.TrangThais.ToList());
        }

        // GET: Admin/TrangThais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThai trangThai = db.TrangThais.Find(id);
            if (trangThai == null)
            {
                return HttpNotFound();
            }
            return View(trangThai);
        }

        // GET: Admin/TrangThais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TrangThais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTrangThai,TenTrangThai")] TrangThai trangThai)
        {
            if (ModelState.IsValid)
            {
                db.TrangThais.Add(trangThai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trangThai);
        }

        // GET: Admin/TrangThais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThai trangThai = db.TrangThais.Find(id);
            if (trangThai == null)
            {
                return HttpNotFound();
            }
            return View(trangThai);
        }

        // POST: Admin/TrangThais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTrangThai,TenTrangThai")] TrangThai trangThai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trangThai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trangThai);
        }

        // GET: Admin/TrangThais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThai trangThai = db.TrangThais.Find(id);
            if (trangThai == null)
            {
                return HttpNotFound();
            }
            return View(trangThai);
        }

        // POST: Admin/TrangThais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrangThai trangThai = db.TrangThais.Find(id);
            db.TrangThais.Remove(trangThai);
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
