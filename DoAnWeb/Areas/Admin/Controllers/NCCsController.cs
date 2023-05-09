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
    public class NCCsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/NCCs
        public ActionResult Index()
        {
            return View(db.NCCs.ToList());
        }

        // GET: Admin/NCCs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCC nCC = db.NCCs.Find(id);
            if (nCC == null)
            {
                return HttpNotFound();
            }
            return View(nCC);
        }

        // GET: Admin/NCCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NCCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,TenNCC,DiaChi,SDT,Email")] NCC nCC)
        {
            if (ModelState.IsValid)
            {
                db.NCCs.Add(nCC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nCC);
        }

        // GET: Admin/NCCs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCC nCC = db.NCCs.Find(id);
            if (nCC == null)
            {
                return HttpNotFound();
            }
            return View(nCC);
        }

        // POST: Admin/NCCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,TenNCC,DiaChi,SDT,Email")] NCC nCC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nCC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nCC);
        }

        // GET: Admin/NCCs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NCC nCC = db.NCCs.Find(id);
            if (nCC == null)
            {
                return HttpNotFound();
            }
            return View(nCC);
        }

        // POST: Admin/NCCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            NCC nCC = db.NCCs.Find(id);
            db.NCCs.Remove(nCC);
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
