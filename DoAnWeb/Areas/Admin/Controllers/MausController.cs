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
    public class MausController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/Maus
        public ActionResult Index()
        {
            return View(db.Maus.ToList());
        }

        // GET: Admin/Maus/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mau mau = db.Maus.Find(id);
            if (mau == null)
            {
                return HttpNotFound();
            }
            return View(mau);
        }

        // GET: Admin/Maus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Maus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMau,TenMau,Anh")] Mau mau)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase Anh = Request.Files["Anh"];
                if(Anh != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                    Anh.SaveAs(path);
                    mau.Anh = Anh.FileName;
                }
                db.Maus.Add(mau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mau);
        }

        // GET: Admin/Maus/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mau mau = db.Maus.Find(id);
            if (mau == null)
            {
                return HttpNotFound();
            }
            return View(mau);
        }

        // POST: Admin/Maus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMau,TenMau,Anh")] Mau mau)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase Anh = Request.Files["Anh"];
                if (Anh != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                    Anh.SaveAs(path);
                    mau.Anh = Anh.FileName;
                }
                db.Entry(mau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mau);
        }

        // GET: Admin/Maus/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mau mau = db.Maus.Find(id);
            if (mau == null)
            {
                return HttpNotFound();
            }
            return View(mau);
        }

        // POST: Admin/Maus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Mau mau = db.Maus.Find(id);
            db.Maus.Remove(mau);
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
