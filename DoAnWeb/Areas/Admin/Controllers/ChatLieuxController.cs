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
    public class ChatLieuxController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/ChatLieux
        public ActionResult Index()
        {
            return View(db.ChatLieux.ToList());
        }

        // GET: Admin/ChatLieux/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // GET: Admin/ChatLieux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChatLieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCL,TenChatLieu")] ChatLieu chatLieu)
        {
            if (ModelState.IsValid)
            {
                db.ChatLieux.Add(chatLieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chatLieu);
        }

        // GET: Admin/ChatLieux/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // POST: Admin/ChatLieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCL,TenChatLieu")] ChatLieu chatLieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chatLieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chatLieu);
        }

        // GET: Admin/ChatLieux/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // POST: Admin/ChatLieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            db.ChatLieux.Remove(chatLieu);
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
