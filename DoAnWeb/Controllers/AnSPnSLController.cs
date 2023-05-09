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
using DoAnWeb.ViewModels;

namespace DoAnWeb.Controllers
{
    public class AnSPnSLController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();
        // GET: AnSP
        public ActionResult Index(int? page, string KeyWord, int? baka)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            AnSPnSL anSPnSL = new AnSPnSL();
            List<ASPSL> aSPSLs = new List<ASPSL>();
            if (KeyWord != null && KeyWord != "")
            {
                sanPhams = db.SanPhams.OrderBy(m => m.MaSP).Where(m => m.TenSP.ToString().Contains(KeyWord)).ToList();
                foreach (var item in sanPhams)
                {
                    ASPSL aSPSL = new ASPSL();
                    aSPSL.Sanss = item;
                    List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    foreach (var c in anhSPs)
                    {
                        aSPSL.Anhh.Add(c);
                    }
                    List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    foreach (var v in soLuongs)
                    {
                        aSPSL.Luongs.Add(v);
                    }
                    aSPSLs.Add(aSPSL);
                }
                anSPnSL.ASPSLs = aSPSLs;
                anSPnSL.Loais = db.LoaiSPs.ToList();
                anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
                anSPnSL.chatLieus = db.ChatLieux.ToList();
                ViewBag.GT = db.GioiTinhs.ToList();
                List<AnSPnSL> anSPnSLs = new List<AnSPnSL>();

                return View(anSPnSL);
            }
            else
            {
                if (baka == 1)
                {
                    sanPhams = db.SanPhams.OrderByDescending(m => m.MaSP).ToList();
                    //foreach (var item in sanPhams)
                    //{
                    //    ASPSL aSPSL = new ASPSL();
                    //    aSPSL.Sanss = item;
                    //    List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    //    foreach (var c in anhSPs)
                    //    {
                    //        aSPSL.Anhh.Add(c);
                    //    }
                    //    List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    //    foreach (var gg in soLuongs)
                    //    {
                    //        SoLuong sl = new SoLuong();
                    //        sl.MaMau = gg.MaMau;
                    //        sl.MaSP = gg.MaSP;
                    //        sl.MaSize = gg.MaSize;
                    //        sl.SoLuong1 = gg.SoLuong1;
                    //        aSPSL.Luongs.Add(sl);
                    //    }
                    //    aSPSLs.Add(aSPSL);
                    //}
                    //anSPnSL.Loais = db.LoaiSPs.ToList();
                    //anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
                    //anSPnSL.ASPSLs = aSPSLs;
                }
                else if(baka == 2)
                {
                    DateTime dt = DateTime.Today.AddMonths(-1);
                    var ban = db.CT_HoaDon.Include(c => c.ChatLieu).Include(c => c.HoaDon).Include(c => c.Mau).Include(c => c.SanPham).Include(c => c.Size).OrderByDescending(o => o.HoaDon.NgayMua > dt).ToList();
                    Dictionary<long, long> check = new Dictionary<long, long>();
                    foreach (var item in ban)
                    {
                        if (check.ContainsKey(item.MaSP))
                        {
                            check[item.MaSP] += long.Parse(item.SoLuong.ToString());
                        }
                        else
                        {
                            check.Add(item.MaSP, long.Parse(item.SoLuong.ToString()));
                        }
                    }
                    check.OrderBy(key => key.Value);
                    
                    foreach(var masp in check)
                    {
                        SanPham item = db.SanPhams.Find(masp.Key);
                        sanPhams.Add(item);
                        //    ASPSL aSPSL = new ASPSL();
                        //    aSPSL.Sanss = item;
                        //    List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                        //    foreach (var c in anhSPs)
                        //    {
                        //        aSPSL.Anhh.Add(c);
                        //    }
                        //    List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                        //    foreach (var gg in soLuongs)
                        //    {
                        //        SoLuong sl = new SoLuong();
                        //        sl.MaMau = gg.MaMau;
                        //        sl.MaSP = gg.MaSP;
                        //        sl.MaSize = gg.MaSize;
                        //        sl.SoLuong1 = gg.SoLuong1;
                        //        aSPSL.Luongs.Add(sl);
                        //    }
                        //    aSPSLs.Add(aSPSL);
                    }
                    //anSPnSL.Loais = db.LoaiSPs.ToList();
                    //anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
                    //anSPnSL.ASPSLs = aSPSLs;
                }
                else if(baka == 3)
                {
                    sanPhams = db.SanPhams.Where(m => m.MaGT == 1).ToList();
                    //foreach (var item in sanPhams)
                    //{
                    //    ASPSL aSPSL = new ASPSL();
                    //    aSPSL.Sanss = item;
                    //    List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    //    foreach (var c in anhSPs)
                    //    {
                    //        aSPSL.Anhh.Add(c);
                    //    }
                    //    List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    //    foreach (var gg in soLuongs)
                    //    {
                    //        SoLuong sl = new SoLuong();
                    //        sl.MaMau = gg.MaMau;
                    //        sl.MaSP = gg.MaSP;
                    //        sl.MaSize = gg.MaSize;
                    //        sl.SoLuong1 = gg.SoLuong1;
                    //        aSPSL.Luongs.Add(sl);
                    //    }
                    //    aSPSLs.Add(aSPSL);
                    //}
                    //anSPnSL.Loais = db.LoaiSPs.ToList();
                    //anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
                    //anSPnSL.ASPSLs = aSPSLs;
                }
                else if(baka == 4)
                {
                    sanPhams = db.SanPhams.Where(m => m.MaGT == 2).ToList();
                    //foreach (var item in sanPhams)
                    //{
                    //    ASPSL aSPSL = new ASPSL();
                    //    aSPSL.Sanss = item;
                    //    List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    //    foreach (var c in anhSPs)
                    //    {
                    //        aSPSL.Anhh.Add(c);
                    //    }
                    //    List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    //    foreach (var gg in soLuongs)
                    //    {
                    //        SoLuong sl = new SoLuong();
                    //        sl.MaMau = gg.MaMau;
                    //        sl.MaSP = gg.MaSP;
                    //        sl.MaSize = gg.MaSize;
                    //        sl.SoLuong1 = gg.SoLuong1;
                    //        aSPSL.Luongs.Add(sl);
                    //    }
                    //    aSPSLs.Add(aSPSL);
                    //}
                    //anSPnSL.Loais = db.LoaiSPs.ToList();
                    //anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
                    //anSPnSL.ASPSLs = aSPSLs;
                }
                else
                {
                    sanPhams = db.SanPhams.OrderBy(m => m.MaSP).ToList();
                    //foreach (var item in sanPhams)
                    //{
                    //    ASPSL aSPSL = new ASPSL();
                    //    aSPSL.Sanss = item;
                    //    List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    //    foreach (var c in anhSPs)
                    //    {
                    //        aSPSL.Anhh.Add(c);
                    //    }
                    //    List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                    //    foreach (var gg in soLuongs)
                    //    {
                    //        SoLuong sl = new SoLuong();
                    //        sl.MaMau = gg.MaMau;
                    //        sl.MaSP = gg.MaSP;
                    //        sl.MaSize = gg.MaSize;
                    //        sl.SoLuong1 = gg.SoLuong1;
                    //        aSPSL.Luongs.Add(sl);
                    //    }
                    //    aSPSLs.Add(aSPSL);          
                    //}
                    //anSPnSL.Loais = db.LoaiSPs.ToList();
                    //anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
                    //anSPnSL.ASPSLs = aSPSLs;
                }
            }
            int pageSize = 20;
            int curPage = 1;
            if (page != null) { curPage = page.Value; }
            int TotalRows = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.GioiTinh).Include(s => s.LoaiSP).Include(s => s.NCC).Include(s => s.ThuongHieu).Count();
            int numPages = TotalRows / pageSize;
            if (TotalRows % pageSize != 0) { numPages = numPages + 1; }
            ViewBag.numPages = numPages;
            ViewBag.currentPage = curPage;
            List<SanPham> sp = sanPhams.Skip((curPage - 1) * pageSize).Take(pageSize).ToList();
            foreach (var item in sp)
            {
                ASPSL aSPSL = new ASPSL();
                aSPSL.Sanss = item;
                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var c in anhSPs)
                {
                    aSPSL.Anhh.Add(c);
                }
                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var v in soLuongs)
                {
                    aSPSL.Luongs.Add(v);
                }
                aSPSLs.Add(aSPSL);
            }
            anSPnSL.Loais = db.LoaiSPs.ToList();
            anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
            anSPnSL.chatLieus = db.ChatLieux.ToList();
            ViewBag.GT = db.GioiTinhs.ToList();
            anSPnSL.ASPSLs = aSPSLs;
            return View(anSPnSL);
        }
        public ActionResult ChiTiet(long id)
        {
            ctSPnCMT thing = new ctSPnCMT();
            var lk = db.SanPhams.Find(id);
            if (lk == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            AnSPnSL aSPSLs = new AnSPnSL();
            List<ASPSL> yess = new List<ASPSL>();
            List<SanPham> hangs = db.SanPhams.Where(m => m.MaLoaiSP == lk.MaLoaiSP).Where(m => m.MaSP != id).OrderByDescending(m => m.MaSP).Take(10).ToList();
            foreach (var item in hangs)
            {
                ASPSL hang = new ASPSL();
                hang.Sanss = item;
                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var c in anhSPs)
                {
                    hang.Anhh.Add(c);
                }
                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var v in soLuongs)
                {
                    hang.Luongs.Add(v);
                }
                yess.Add(hang);
            }
            aSPSLs.ASPSLs = yess;
            aSPSLs.Loais = db.LoaiSPs.ToList();
            aSPSLs.ThuongHieus = db.ThuongHieux.ToList();
            ViewBag.Soluong = new SelectList(db.SoLuongs, "MaMau", "MaSize", db.SoLuongs.Where(m => m.MaSP.Equals(id)));
            ASPSL aSPSL = new ASPSL();
            aSPSL.Anhh = db.AnhSPs.Where(m => m.MaSP.Equals(id)).ToList();
            aSPSL.Luongs = db.SoLuongs.Where(m => m.MaSP.Equals(id)).Where(m => m.SoLuong1 > 0).ToList();
            aSPSL.Sanss = db.SanPhams.Find(id);
            ctSP sanphamm = new ctSP();
            sanphamm.sanp = aSPSL;
            sanphamm.lqs = aSPSLs;
            thing.sanpham = sanphamm;
            thing.cmt = db.Comments.Where(m => m.MaSP == id).ToList();
            return View(thing);
        }
        [HttpPost]
        public ActionResult Chot(FormCollection forms)
        {
            if (Session["user_name"] == null)
            {
                string abc = Url.Action("ChiTiet", new { id = long.Parse(forms[0].ToString()) });
                return RedirectToAction("Index", "Login", new { returnurl = abc });
            }
            long k = long.Parse(Session["user_name"].ToString());
            SanPham sanPham = db.SanPhams.Find(long.Parse(forms[0].ToString()));
            GioHang gioHang = new GioHang();
            int a = int.Parse(forms[2].ToString());
            long b = long.Parse(forms[1].ToString());
            var ghc = db.GioHangs.Where(m => m.MaTK.Equals(k)).ToList();
            if(ghc == null)
            {
                gioHang.MaSP = sanPham.MaSP;
                gioHang.MaSize = int.Parse(forms[2].ToString());
                gioHang.MaTK = long.Parse(Session["user_name"].ToString());
                gioHang.MaTK = long.Parse(Session["user_name"].ToString());
                gioHang.MaMau = long.Parse(forms[1].ToString());
                gioHang.SoLuong = long.Parse(forms[3].ToString());
                gioHang.MaCL = sanPham.MaCL;
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
            }
            else
            {
                bool stronger = false;
                foreach (var item in ghc)
                {
                    if (item.MaMau == long.Parse(forms[1].ToString()) && item.MaSize == int.Parse(forms[2].ToString()) && item.MaSP ==  long.Parse(forms[0].ToString()))
                    {
                        db.GioHangs.Remove(item);
                        db.SaveChanges();
                        item.SoLuong = item.SoLuong + long.Parse(forms[3].ToString());
                        db.GioHangs.Add(item);
                        db.SaveChanges();
                        stronger = true;
                        break;
                    }
                    
                }
                if(stronger==false)
                {
                    gioHang.MaSP = sanPham.MaSP;
                    gioHang.MaSize = int.Parse(forms[2].ToString());
                    gioHang.MaTK = long.Parse(Session["user_name"].ToString());
                    gioHang.MaMau = long.Parse(forms[1].ToString());
                    gioHang.SoLuong = long.Parse(forms[3].ToString());
                    gioHang.MaCL = sanPham.MaCL;
                    db.GioHangs.Add(gioHang);
                    
                    db.SaveChanges();
                }
            }
            

            return RedirectToAction("Index", "GioHangKHs");
        }

        public ActionResult chonmau(long mau, long masp)
        {
            List<SoLuong> sls = db.SoLuongs.Where(m => m.MaSP.Equals(masp)).Where(m => m.SoLuong1 > 0).Where(m => m.MaMau == mau).ToList();
            return PartialView("_Size", sls);
        }
        public ActionResult chonsize(int size, long masp)
        {
            ASPSL aSPSL = new ASPSL();
            aSPSL.Luongs = db.SoLuongs.Where(m => m.MaSP.Equals(masp)).Where(m => m.SoLuong1 > 0).Where(m => m.MaSize == size).ToList();
            List<AnhSP> anhSPs = new List<AnhSP>();
            foreach(var item in aSPSL.Luongs)
            {
                anhSPs.Add(db.AnhSPs.Find(item.MaSP, item.MaMau));
            }
            aSPSL.Anhh = anhSPs;
            aSPSL.Sanss = db.SanPhams.Find(masp);
            return PartialView("_Mau", aSPSL);
        }
        
        public JsonResult tinhSLMau(long masp, long mau, long size)
        {
            if(db.SoLuongs.Find(masp, mau, size) == null)
            {
                return Json(new { xoa = size });
            }
            long sl = long.Parse(db.SoLuongs.Find(masp, mau, size).SoLuong1.ToString());
            return Json(new { soLuong = sl });
        }

        public JsonResult tinhSLSize(long masp, long mau, long size)
        {
            if (db.SoLuongs.Find(masp, mau, size) == null)
            {
                return Json(new { xoa = mau });
            }
            long sl = long.Parse(db.SoLuongs.Find(masp, mau, size).SoLuong1.ToString());
            return Json(new { soLuong = sl });
        }


        public ActionResult filter(long id)
        {
            AnSPnSL anSPnSL = new AnSPnSL();
            List<ASPSL> aSPSLs = new List<ASPSL>();
            var sans = db.SanPhams.Where(m => m.MaLoaiSP == id).ToList();
            foreach (var item in sans)
            {
                ASPSL aSPSL = new ASPSL();
                aSPSL.Sanss = item;
                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var c in anhSPs)
                {
                    aSPSL.Anhh.Add(c);
                }
                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var gg in soLuongs)
                {
                    SoLuong sl = new SoLuong();
                    sl.MaMau = gg.MaMau;
                    sl.MaSP = gg.MaSP;
                    sl.MaSize = gg.MaSize;
                    sl.SoLuong1 = gg.SoLuong1;
                    aSPSL.Luongs.Add(sl);
                }
                aSPSLs.Add(aSPSL);
            }
            anSPnSL.ASPSLs = aSPSLs;
            anSPnSL.Loais = db.LoaiSPs.ToList();
            anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
            anSPnSL.chatLieus = db.ChatLieux.ToList();
            return View("Index", anSPnSL);
        }
        public ActionResult filTH(long id)
        {
            AnSPnSL anSPnSL = new AnSPnSL();
            List<ASPSL> aSPSLs = new List<ASPSL>();
            var sans = db.SanPhams.Where(m => m.MaTH == id).ToList();
            foreach (var item in sans)
            {
                ASPSL aSPSL = new ASPSL();
                aSPSL.Sanss = item;
                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var c in anhSPs)
                {
                    aSPSL.Anhh.Add(c);
                }
                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var gg in soLuongs)
                {
                    SoLuong sl = new SoLuong();
                    sl.MaMau = gg.MaMau;
                    sl.MaSP = gg.MaSP;
                    sl.MaSize = gg.MaSize;
                    sl.SoLuong1 = gg.SoLuong1;
                    aSPSL.Luongs.Add(sl);
                }
                aSPSLs.Add(aSPSL);
            }
            anSPnSL.ASPSLs = aSPSLs;
            anSPnSL.Loais = db.LoaiSPs.ToList();
            anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
            anSPnSL.chatLieus = db.ChatLieux.ToList();
            return View("Index", anSPnSL);
        }
        [HttpPost]
        public ActionResult Search(FormCollection fom)
        {
            var sans = db.SanPhams.Where(m => m.TenSP.Contains(fom["te"])).ToList();
            AnSPnSL anSPnSL = new AnSPnSL();
            List<ASPSL> aSPSLs = new List<ASPSL>();
            foreach (var item in sans)
            {
                ASPSL aSPSL = new ASPSL();
                aSPSL.Sanss = item;
                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var c in anhSPs)
                {
                    aSPSL.Anhh.Add(c);
                }
                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                foreach (var gg in soLuongs)
                {
                    SoLuong sl = new SoLuong();
                    sl.MaMau = gg.MaMau;
                    sl.MaSP = gg.MaSP;
                    sl.MaSize = gg.MaSize;
                    sl.SoLuong1 = gg.SoLuong1;
                    aSPSL.Luongs.Add(sl);
                }
                aSPSLs.Add(aSPSL);
            }
            anSPnSL.ASPSLs = aSPSLs;
            anSPnSL.Loais = db.LoaiSPs.ToList();
            anSPnSL.ThuongHieus = db.ThuongHieux.ToList();
            anSPnSL.chatLieus = db.ChatLieux.ToList();
            return View("Index", anSPnSL);
        }
        public ActionResult realfilter(long th, long cl, long lsp, int gt)
        {
            if (gt != 0)
            {
                if (th != 0)
                {
                    if (cl != 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).Where(m => m.MaCL == cl).Where(m => m.MaGT == gt).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).Where(m => m.MaCL == cl).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp == 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).Where(m => m.MaLoaiSP == lsp).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
                else
                {
                    if (cl == 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaLoaiSP == lsp).Where(m => m.MaGT == gt).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaGT == gt).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaCL == th).Where(m => m.MaGT == gt).Where(m => m.MaLoaiSP == lsp).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaCL == cl).Where(m => m.MaGT == gt).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
            }
            else
            {
                if (th != 0)
                {
                    if (cl != 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).Where(m => m.MaCL == cl).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaCL == cl).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp == 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
                else
                {
                    if (cl == 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaLoaiSP == lsp).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaCL == th).Where(m => m.MaLoaiSP == lsp).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaCL == cl).ToList();
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
            }
        }

        public ActionResult realfilternew(long th, long cl, long lsp, int gt, int bro)
        {
            if (gt != 0)
            {
                if (th != 0)
                {
                    if (cl != 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).Where(m => m.MaCL == cl).Where(m => m.MaGT == gt).ToList();
                            if(bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach(var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach(var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                //foreach (var item in ban)
                                //{
                                //    if (check.ContainsKey(item.MaSP))
                                //    {
                                //        check[item.MaSP] += long.Parse(item.SoLuong.ToString());
                                //    }
                                //    else
                                //    {
                                //        check.Add(item.MaSP, long.Parse(item.SoLuong.ToString()));
                                //    }
                                //}
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).Where(m => m.MaCL == cl).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp == 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).Where(m => m.MaLoaiSP == lsp).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
                else
                {
                    if (cl == 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaLoaiSP == lsp).Where(m => m.MaGT == gt).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaGT == gt).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaCL == th).Where(m => m.MaGT == gt).Where(m => m.MaLoaiSP == lsp).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaCL == cl).Where(m => m.MaGT == gt).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
            }
            else
            {
                if (th != 0)
                {
                    if (cl != 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).Where(m => m.MaCL == cl).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaCL == cl).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp == 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
                else
                {
                    if (cl == 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaLoaiSP == lsp).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaCL == th).Where(m => m.MaLoaiSP == lsp).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaCL == cl).ToList();
                            if (bro == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.MaSP).ToList();
                            }
                            else
                            {
                                DateTime dt = DateTime.Today.AddMonths(-1);
                                Dictionary<long, long> check = new Dictionary<long, long>();
                                foreach (var temi in hangs)
                                {
                                    long slBan = 0;
                                    foreach (var thing in temi.CT_HoaDon)
                                    {
                                        slBan += long.Parse(thing.SoLuong.ToString());
                                    }
                                    check.Add(temi.MaSP, slBan);
                                }
                                check.OrderBy(key => key.Value);
                                hangs.Clear();
                                foreach (var masp in check)
                                {
                                    SanPham item = db.SanPhams.Find(masp.Key);
                                    hangs.Add(item);
                                }
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
            }
        }

        public ActionResult moneh(long th, long cl, long lsp, int gt, int paradise)
        {
            if (gt != 0)
            {
                if (th != 0)
                {
                    if (cl != 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).Where(m => m.MaCL == cl).Where(m => m.MaGT == gt).ToList();
                            if(paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            List<SanPham> hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).Where(m => m.MaCL == cl).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp == 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaGT == gt).Where(m => m.MaLoaiSP == lsp).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
                else
                {
                    if (cl == 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaLoaiSP == lsp).Where(m => m.MaGT == gt).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaGT == gt).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaCL == th).Where(m => m.MaGT == gt).Where(m => m.MaLoaiSP == lsp).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaCL == cl).Where(m => m.MaGT == gt).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
            }
            else
            {
                if (th != 0)
                {
                    if (cl != 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).Where(m => m.MaCL == cl).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaCL == cl).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp == 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaTH == th).Where(m => m.MaLoaiSP == lsp).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
                else
                {
                    if (cl == 0)
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaLoaiSP == lsp).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                    else
                    {
                        if (lsp != 0)
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaCL == th).Where(m => m.MaLoaiSP == lsp).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                        else
                        {
                            List<ASPSL> aSPSLs = new List<ASPSL>();
                            var hangs = db.SanPhams.Where(m => m.MaCL == cl).ToList();
                            if (paradise == 1)
                            {
                                hangs = hangs.OrderByDescending(m => m.DonGia).ToList();
                            }
                            else
                            {
                                hangs = hangs.OrderBy(m => m.DonGia).ToList();
                            }
                            foreach (var item in hangs)
                            {
                                ASPSL aSPSL = new ASPSL();
                                aSPSL.Sanss = item;
                                List<AnhSP> anhSPs = db.AnhSPs.OrderBy(m => m.MaSP).Include(a => a.Mau).Include(a => a.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var anh in anhSPs)
                                {
                                    aSPSL.Anhh.Add(anh);
                                }
                                List<SoLuong> soLuongs = db.SoLuongs.OrderBy(m => m.MaSP).Include(s => s.Mau).Include(s => s.Size).Include(s => s.SanPham).Where(m => m.MaSP.Equals(item.MaSP)).ToList();
                                foreach (var gg in soLuongs)
                                {
                                    SoLuong sl = new SoLuong();
                                    sl.MaMau = gg.MaMau;
                                    sl.MaSP = gg.MaSP;
                                    sl.MaSize = gg.MaSize;
                                    sl.SoLuong1 = gg.SoLuong1;
                                    aSPSL.Luongs.Add(sl);
                                }
                                aSPSLs.Add(aSPSL);
                            }
                            return PartialView("_SanPham", aSPSLs);
                        }
                    }
                }
            }
        }
        public ActionResult CCMT(string cmt, long masp, int rate)
        {
            Comment comment = new Comment();
            comment.MaTK = long.Parse(Session["user_name"].ToString());
            comment.CMT = cmt;
            comment.DanhGia = rate;
            comment.Date = DateTime.Now;
            comment.MaSP = masp;
            db.Comments.Add(comment);
            db.SaveChanges();
            var cmts = db.Comments.Where(m => m.MaSP == masp).ToList();
            return PartialView("_Comment", cmts);
        }
        public ActionResult CRepCMT(int macmt, string cmt, int masp)
        {
            RepCMT rep = new RepCMT();
            rep.MaCMT = macmt;
            rep.MaTK = long.Parse(Session["user_name"].ToString());
            rep.NoiDung = cmt;
            rep.Date = DateTime.Now;
            db.RepCMTs.Add(rep);
            db.SaveChanges();
            var repcmts = db.RepCMTs.Where(m => m.MaCMT == macmt).Include(m => m.TaiKhoan);
            return PartialView("_RepCMT", repcmts);
        }
    }
}