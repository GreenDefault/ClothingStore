using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.Controllers
{
    public class SanPhamKHsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: SanPhamKHs
        public ActionResult Index(int? page, string KeyWord)
        {
            List<SanPham> lsp = new List<SanPham>();
            if (page != null && KeyWord != "")
            {
                lsp = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.GioiTinh).Include(s => s.LoaiSP).Include(s => s.NCC).Include(s => s.ThuongHieu).ToList();
                return View(lsp);
            }
            else
            {
                lsp = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.GioiTinh).Include(s => s.LoaiSP).Include(s => s.NCC).Include(s => s.ThuongHieu).ToList();
            }
            int pageSize = 5;
            int curPage = 1;
            if (page != null) { curPage = page.Value; }
            int TotalRows = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.GioiTinh).Include(s => s.LoaiSP).Include(s => s.NCC).Include(s => s.ThuongHieu).Count();
            int numPages = TotalRows / pageSize;
            if (TotalRows % pageSize != 0) { numPages = numPages + 1; }
            ViewBag.numPages = numPages;
            ViewBag.currentPage = curPage;
            lsp = db.SanPhams.OrderBy(model => model.MaSP).Skip((curPage - 1) * pageSize).Take(pageSize).ToList();
            return View(lsp);
        }

        // GET: SanPhamKHs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhamKHs/Create
        public ActionResult Create()
        {
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu");
            ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC");
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH");
            return View();
        }

        // POST: SanPhamKHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,DonGia,MaLoaiSP,MaCL,MaNCC,MaTH,MaGT")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", sanPham.MaCL);
            ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1", sanPham.MaGT);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // GET: SanPhamKHs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", sanPham.MaCL);
            ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1", sanPham.MaGT);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // POST: SanPhamKHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,DonGia,MaLoaiSP,MaCL,MaNCC,MaTH,MaGT")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", sanPham.MaCL);
            ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1", sanPham.MaGT);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
            return View(sanPham);
        }

        // GET: SanPhamKHs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhamKHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
        public ActionResult Home()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
