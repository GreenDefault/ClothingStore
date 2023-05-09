using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;
using DoAnWeb.Areas.Admin.ViewModels;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.GioiTinh).Include(s => s.LoaiSP).Include(s => s.NCC).Include(s => s.ThuongHieu);
            return View(sanPhams.ToList());
        }

        // GET: Admin/SanPhams/Details/5
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

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu");
            ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC");
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH");
            return View();
        }

        // POST: Admin/SanPhams/Create
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

        // GET: Admin/SanPhams/Edit/5
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
            ViewBag.MaMau0 = new SelectList(db.Maus, "MaMau", "TenMau");
            ViewBag.Size0 = new SelectList(db.Sizes, "MaSize", "TenSize");
            var asdas = new SelectList(db.Sizes, "MaSize", "TenSize");
            losAngeles doc = new losAngeles();
            doc.sp = db.SanPhams.Find(id);
            var sls = db.SoLuongs.Where(m => m.MaSP == id);
            List<Tuple<long, int, long, string>> aas = new List<Tuple<long, int, long, string>>();
            foreach (var item in sls)
            {
                AnhSP anhSP = db.AnhSPs.Find(item.MaSP, item.MaMau);
                aas.Add(Tuple.Create(item.MaMau, item.MaSize, long.Parse(item.SoLuong1.ToString()), anhSP.Anh));
            }
            doc.anh = aas;
            return View(doc);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection forms)
        {
            SanPham sanPham = db.SanPhams.Find(long.Parse(forms[1]));
            if (ModelState.IsValid)
            {
                sanPham.TenSP = forms[2];
                sanPham.DonGia = long.Parse(forms[3]);
                sanPham.MaLoaiSP = long.Parse(forms[4]);
                sanPham.MaCL = long.Parse(forms[5]);
                sanPham.MaNCC = long.Parse(forms[6]);
                sanPham.MaTH = long.Parse(forms[7]);
                sanPham.MaGT = int.Parse(forms[8]);
                db.Entry(sanPham).State = EntityState.Modified;
                var dd = int.Parse(forms["addnew"].ToString());
                var sizeee = db.Sizes.ToList();
                var sizes = sizeee.Count();
                var sls = db.SoLuongs.Where(m => m.MaSP == sanPham.MaSP).ToList();
                var currentMau = int.Parse(forms["oldremain"].ToString());
                var anh = 1;
                if (currentMau <= 0)
                {
                    foreach (var item in sls)
                    {
                        db.SoLuongs.Remove(item);
                    }
                    var abc = db.AnhSPs.Where(m => m.MaSP == sanPham.MaSP).ToList();
                    foreach (var thingy in abc)
                    {
                        db.AnhSPs.Remove(thingy);
                    }
                    for (int i = 9; i < forms.Count - 2; i = i+0)
                    {
                        int mau = i;
                        i++;
                        foreach (var temi in sizeee)
                        {
                            if (!string.IsNullOrEmpty(forms[i]))
                            {
                                SoLuong sl = new SoLuong();
                                sl.MaSP = sanPham.MaSP;
                                sl.MaMau = int.Parse(forms[mau].ToString());
                                sl.MaSize = temi.MaSize;
                                sl.SoLuong1 = long.Parse(forms[i]);
                                db.SoLuongs.Add(sl);
                                i++;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        HttpPostedFileBase Anh = Request.Files["Anh" + anh];
                        anh++;
                        if (Anh.ContentLength > 0)
                        {
                            AnhSP anhSP = new AnhSP();
                            string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                            Anh.SaveAs(path);
                            anhSP.MaMau = long.Parse(forms[mau]);
                            anhSP.Anh = Anh.FileName;
                            db.AnhSPs.Add(anhSP);
                        }
                        else
                        {
                            AnhSP anhSP = new AnhSP();
                            anhSP.MaMau = long.Parse(forms[mau]);
                            anhSP.Anh = "";
                            db.AnhSPs.Add(anhSP);
                        }
                    }
                    ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", sanPham.MaCL);
                    ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1", sanPham.MaGT);
                    ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
                    ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", sanPham.MaNCC);
                    ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
                    return RedirectToAction("Edit", sanPham.MaSP);
                }
                else
                {
                    foreach(var understand in sls)
                    {
                        db.SoLuongs.Remove(understand);
                    }
                    int anhrm = 1;
                    int i;
                    for(i=9; i< 9 + (currentMau * sizes) + (currentMau * 2) - 1; i = i + 1)
                    {
                        var oldAnh = db.AnhSPs.Find(sanPham.MaSP, int.Parse(forms[i + sizes + 1]));
                        AnhSP newAnh = new AnhSP();
                        newAnh.MaSP = sanPham.MaSP;
                        db.AnhSPs.Remove(oldAnh);
                        newAnh.MaMau = int.Parse(forms[i]);
                        HttpPostedFileBase Anh = Request.Files["Anh " + anhrm];
                        anhrm++;
                        if (Anh != null && Anh.ContentLength>0)
                        {
                            string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                            Anh.SaveAs(path);
                            newAnh.Anh = Anh.FileName;
                        }
                        else
                        {
                            newAnh.Anh = oldAnh.Anh;
                        }
                        //string sql = "Update AnhSP set MaMau = '" + newAnh.MaMau + "' where MaSP = '" + sanPham.MaSP + "' and MaMau = '" + int.Parse(forms[i + 6]) + "'";
                        //db.AnhSPs.SqlQuery(sql);
                        db.AnhSPs.Add(newAnh);
                        var mau = i;
                        //var damnulinq = int.Parse(forms[i + sizes + 1]);
                        //var amer = db.SoLuongs.Where(m => m.MaSP == sanPham.MaSP).Where(m => m.MaMau == damnulinq).ToList();
                        //foreach(var temi in amer)
                        //{
                        //    db.SoLuongs.Remove(temi);
                        //}
                        i++;
                        foreach (var item in sizeee)
                        {
                            if (!string.IsNullOrEmpty(forms[i]))
                            {
                                SoLuong sl = new SoLuong();
                                sl.MaSP = sanPham.MaSP;
                                sl.MaMau = int.Parse(forms[mau].ToString());
                                sl.MaSize = item.MaSize;
                                sl.SoLuong1 = long.Parse(forms[i]);
                                db.SoLuongs.Add(sl);
                                i++;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                    for(int j = i; j<forms.Count-2; j= j+0)
                    {
                        int mau = j;
                        j++;
                        HttpPostedFileBase Anh = Request.Files["Anh" + anh];
                        if(Anh.ContentLength>0)
                        {
                            AnhSP anhSP = new AnhSP();
                            anhSP.MaMau = int.Parse(forms[mau]);
                            anhSP.MaSP = sanPham.MaSP;
                            anhSP.Anh = Anh.FileName;
                            db.AnhSPs.Add(anhSP);
                            string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                            Anh.SaveAs(path);

                        }
                        else
                        {
                            AnhSP anhSP = new AnhSP();
                            anhSP.MaMau = int.Parse(forms[mau]);
                            anhSP.MaSP = sanPham.MaSP;
                            anhSP.Anh = "";
                            db.AnhSPs.Add(anhSP);
                        }
                        foreach(var item in sizeee)
                        {
                            if (!string.IsNullOrEmpty(forms[j]))
                            {
                                SoLuong sl = new SoLuong();
                                sl.MaSP = sanPham.MaSP;
                                sl.MaMau = int.Parse(forms[mau].ToString());
                                sl.MaSize = item.MaSize;
                                sl.SoLuong1 = long.Parse(forms[j]);
                                db.SoLuongs.Add(sl);
                                j++;
                            }
                            else
                            {
                                j++;
                            }
                        }
                    }
                    
                }
                //foreach(var item in sls)
                //{
                //    db.SoLuongs.Remove(item);
                //}
                //var currentI = 9;
                //for(int i=9; i<9+(currentMau*sizes)+(currentMau*2)+currentMau-1; i=i+(sizes+3))
                //{
                //    foreach(var thing in sizeee)
                //    {

                //    }
                //    SoLuong sl = new SoLuong();
                //    sl.MaSP = sanPham.MaSP;
                //    sl.MaMau = int.Parse(forms[i].ToString());
                //    sl.MaSize = sizeee[sizee].MaSize;
                //    sl.SoLuong1 = long.Parse(forms[i + sizee + 1].ToString());
                //    sizee++;
                //}
                db.SaveChanges();
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", sanPham.MaCL);
            ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1", sanPham.MaGT);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", sanPham.MaTH);
            return RedirectToAction("Edit", sanPham.MaSP);
        }

        // GET: Admin/SanPhams/Delete/5
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

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            List<SoLuong> soLuongs = db.SoLuongs.Where(m => m.MaSP.Equals(id)).ToList();
            List<AnhSP> anhSPs = db.AnhSPs.Where(m => m.MaSP.Equals(id)).ToList();
            List<CT_HoaDon> cT_HoaDons = db.CT_HoaDon.Where(m => m.MaSP == id).ToList();
            List<GioHang> gioHangs = db.GioHangs.Where(m => m.MaSP == id).ToList();
            List<Comment> comments = db.Comments.Where(m => m.MaSP == id).ToList();
            foreach (var item in cT_HoaDons)
            {
                db.CT_HoaDon.Remove(item);
            }
            foreach (var item in gioHangs)
            {
                db.GioHangs.Remove(item);
            }
            foreach (var item in soLuongs)
            {
                db.SoLuongs.Remove(item);
            }
            foreach (var item in anhSPs)
            {
                db.AnhSPs.Remove(item);
            }
            foreach(var item in comments)
            {
                foreach(var temi in item.RepCMTs.ToList())
                {
                    db.RepCMTs.Remove(temi);
                }
                db.Comments.Remove(item);
            }
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
    }
}
