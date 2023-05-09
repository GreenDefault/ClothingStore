using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;
using DoAnWeb.ViewModels;

namespace DoAnWeb.Controllers
{
    public class GioHangKHsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: GioHangKHs
        public ActionResult Index()
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            long k = long.Parse(Session["user_name"].ToString());
            var gioHangs = db.GioHangs.Include(g => g.ChatLieu).Include(g => g.Mau).Include(g => g.Size).Include(g => g.TaiKhoan).Include(g => g.SanPham).Where(m => m.MaTK.Equals(k)).ToList();
            GHnA gHnA = new GHnA();
            foreach (var item in gioHangs)
            {
                onlyreason only = new onlyreason();
                only.gh = item;
                AnhSP anhSP = db.AnhSPs.Find(item.MaSP, item.MaMau);
                only.anh = anhSP.Anh;
                gHnA.nobody.Add(only);
            }
            return View(gHnA);
        }

            // GET: GioHangKHs/Details/5
            public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // GET: GioHangKHs/Create
        public ActionResult Create()
        {
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu");
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize");
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN");
            return View();
        }

        // POST: GioHangKHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTK,MaSP,MaCL,MaSize,MaMau,SoLuong,ThanhTien")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", gioHang.MaCL);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", gioHang.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", gioHang.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", gioHang.MaSize);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", gioHang.MaTK);
            return View(gioHang);
        }

        // GET: GioHangKHs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", gioHang.MaCL);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", gioHang.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", gioHang.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", gioHang.MaSize);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", gioHang.MaTK);
            return View(gioHang);
        }

        // POST: GioHangKHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTK,MaSP,MaCL,MaSize,MaMau,SoLuong,ThanhTien")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", gioHang.MaCL);
            ViewBag.MaMau = new SelectList(db.Maus, "MaMau", "TenMau", gioHang.MaMau);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", gioHang.MaSP);
            ViewBag.MaSize = new SelectList(db.Sizes, "MaSize", "TenSize", gioHang.MaSize);
            ViewBag.MaTK = new SelectList(db.TaiKhoans, "MaTK", "TenDN", gioHang.MaTK);
            return View(gioHang);
        }

        // GET: GioHangKHs/Delete/5
        public ActionResult Delete(long di, long cd, int dc)
        {
            long matk = long.Parse(Session["user_name"].ToString());
            GioHang gioHang = db.GioHangs.Find(matk, di, cd, dc);
            db.GioHangs.Remove(gioHang);
            db.SaveChanges();
            long k = long.Parse(Session["user_name"].ToString());
            var gioHangs = db.GioHangs.Include(g => g.ChatLieu).Include(g => g.Mau).Include(g => g.Size).Include(g => g.TaiKhoan).Include(g => g.SanPham).Where(m => m.MaTK.Equals(k)).ToList();
            List<onlyreason> things = new List<onlyreason>();
            foreach (var item in gioHangs)
            {
                onlyreason only = new onlyreason();
                only.gh = item;
                AnhSP anhSP = db.AnhSPs.Find(item.MaSP, item.MaMau);
                only.anh = anhSP.Anh;
                things.Add(only);
            }
            return PartialView("_ThongTinGioHang", things);
        }

        // POST: GioHangKHs/Delete/5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost, ActionName("buyall")]
        public ActionResult buyall(FormCollection things)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            long k = long.Parse(Session["user_name"].ToString());
            var a = things["all"];
            var gioHangs = db.GioHangs.Include(g => g.SanPham).Where(m => m.MaTK.Equals(k));
            List<Tuple<long, long, int, long>> vals = new List<Tuple<long, long, int, long>>();
            List<Cokess> hangs = new List<Cokess>();
            ViewModel_TTnGH tt = new ViewModel_TTnGH();
            var ghs = gioHangs.ToList();
            int dem = 1;
            foreach (var gh in ghs)
            {
                if (dem == int.Parse(a))
                {
                    break;
                }
                if (things["ckb 0"] != null)
                {
                    string test = gh.MaMau.ToString() + " " + gh.MaSize.ToString() + " " + gh.MaSP.ToString();
                    if (int.Parse(things[test]) != 0)
                    {
                        Cokess coca = new Cokess();
                        gh.SoLuong = long.Parse(things[test]);
                        gh.ThanhTien = gh.SoLuong * gh.SanPham.DonGia;
                        db.Entry(gh).State = EntityState.Modified;
                        db.SaveChanges();
                        coca.gh = gh;
                        coca.th = gh.SanPham.ThuongHieu.TenTH;
                        coca.mau = gh.Mau.TenMau;
                        coca.size = gh.Size.TenSize;
                        AnhSP anhspsp = db.AnhSPs.Find(gh.MaSP, gh.MaMau);
                        coca.anh = anhspsp.Anh;
                        hangs.Add(coca);
                    }
                }
                else
                {
                    string bbc = "ckb " + dem;
                    if (things[bbc] != null)
                    {
                        string test = gh.MaMau.ToString() + " " + gh.MaSize.ToString() + " " + gh.MaSP.ToString();
                        if (int.Parse(things[test]) != 0)
                        {
                            Cokess coca = new Cokess();
                            gh.SoLuong = long.Parse(things[test]);
                            gh.ThanhTien = gh.SoLuong * gh.SanPham.DonGia;
                            db.Entry(gh).State = EntityState.Modified;
                            db.SaveChanges();
                            coca.gh = gh;
                            coca.th = gh.SanPham.ThuongHieu.TenTH;
                            coca.mau = gh.Mau.TenMau;
                            coca.size = gh.Size.TenSize;
                            AnhSP anhspsp = db.AnhSPs.Find(gh.MaSP, gh.MaMau);
                            coca.anh = anhspsp.Anh;
                            hangs.Add(coca);
                        }
                    }
                }
                dem++;
            }

            //foreach (var hang in hangs)
            //{
            //    db.GioHangs.Remove(hang);
            //}
            Session["Cart"] = hangs;
            return RedirectToAction("Index", "TTnGH");
        }

        public ActionResult Offline ()
        {
            return View();
        }

        public ActionResult checkout()
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            long k = long.Parse(Session["user_name"].ToString());
            var gioHangs = db.GioHangs.Include(g => g.SanPham).Where(m => m.MaTK.Equals(k));
            return View(gioHangs.ToList());
        }
        [HttpPost, ActionName("confirm")]
        public ActionResult confirm(string cba)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            if (cba == "1")
            {
                long k = long.Parse(Session["user_name"].ToString());
                var gioHangs = db.GioHangs.Include(g => g.SanPham).Where(m => m.MaTK.Equals(k));
                var ghs = gioHangs.ToList();
                List<GioHang> buys = new List<GioHang>();
                foreach (var gh in ghs)
                {
                    if (gh.SoLuong > 0)
                    {
                        buys.Add(gh);
                    }
                }
                HoaDon hd = new HoaDon();
                hd.MaThe = 1;
                hd.MaTK = k;
                hd.NgayMua = DateTime.Now;
                hd.TongTien = 0;
                db.HoaDons.Add(hd);
                foreach (var buy in buys)
                {
                    CT_HoaDon cthd = new CT_HoaDon();
                    cthd.MaSP = buy.MaSP;
                    cthd.SoLuong = buy.SoLuong;
                    cthd.ThanhTien = buy.ThanhTien;
                    db.CT_HoaDon.Add(cthd);
                    db.GioHangs.Remove(buy);
                }
                db.SaveChanges();
                return RedirectToAction("Offline");
            }
            else
            {
                return RedirectToAction("Online");
            }
        }
        public ActionResult History()
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            var k = long.Parse(Session["user_name"].ToString());
            var trangthais = db.TrangThais.ToList();
            var tk = db.TaiKhoans.Find(k);
            TTnTK hdtk = new TTnTK();
            hdtk.tt = trangthais;
            hdtk.tk = tk;
            return View(hdtk);
        }
        public ActionResult Huy(long id)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            var del = db.CT_HoaDon.Where(m => m.MaHD == id).ToList();
            foreach (var item in del)
            {
                db.CT_HoaDon.Remove(item);
            }
            db.HoaDons.Remove(db.HoaDons.Find(id));
            db.SaveChanges();
            return RedirectToAction("History");
        }
        public ActionResult xn(long id)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            hoaDon.TrangThai = 7;
            db.Entry(hoaDon).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("History");
        }
        public ActionResult DetailHistory(long id)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            return View(db.CT_HoaDon.Where(m => m.MaHD == id).ToList());
        }

        public ActionResult filter(long id)
        {
            if(id == -1)
            {
                var tk = db.TaiKhoans.Find(long.Parse(Session["user_name"].ToString()));
                return PartialView("_ThongTinKH", tk);
            }
            if(id == -2)
            {
                return PartialView("_DoiMK");
            }
            long matk = long.Parse(Session["user_name"].ToString());
            var hoadons = db.HoaDons.Where(m => m.TrangThai == id).Where(m=> m.MaTK == matk).ToList();

            return PartialView("_LocHD", hoadons);
        }
        public ActionResult xoahd(long id, long tt)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            var del = db.CT_HoaDon.Where(m => m.MaHD == id).ToList();
            foreach (var item in del)
            {
                db.CT_HoaDon.Remove(item);
            }
            db.HoaDons.Remove(db.HoaDons.Find(id));
            db.SaveChanges();
            long matk = long.Parse(Session["user_name"].ToString());
            var hoadons = db.HoaDons.Where(m => m.TrangThai == tt).Where(m => m.MaTK == matk).ToList();
            return PartialView("_LocHD", hoadons);
        }

        public ActionResult nhanhang(long id)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            var confirm = db.HoaDons.Find(id);
            confirm.TrangThai = 7;
            db.Entry(confirm).State = EntityState.Modified;
            db.SaveChanges();
            long matk = long.Parse(Session["user_name"].ToString());
            var hoadons = db.HoaDons.Where(m => m.TrangThai == 6).Where(m => m.MaTK == matk).ToList();
            return PartialView("_LocHD", hoadons);
        }

        public JsonResult Doimk(string currentpass, string newpass)
        {
            if (Session["user_name"] == null)
            {
                return Json(new { status = "Hết phiên đăng nhập! Vui lòng tải lại trang" });
            }
            var taikhoan = db.TaiKhoans.Find(long.Parse(Session["user_name"].ToString()));
            if (currentpass == taikhoan.MatKhau)
            {
                taikhoan.MatKhau = newpass;
                db.Entry(taikhoan).State = EntityState.Modified;
                return Json(new { status = "Đổi mật khẩu thành công!" });
            }
            return Json(new { status = "Đổi mật khẩu thất bại!" });
        }

        public ActionResult Online()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMo.MoMoSecurity crypto = new MoMo.MoMoSecurity();
            string secretKey = ConfigurationManager.AppSettings["secretKey"].ToString();
            string signatrue = crypto.signSHA256(param, secretKey);
            if (signatrue != Request["signature"])
            {
                ViewBag.message = "Thông tin không hợp lệ!";
                return View();
            }
            if (Request["errorCode"] != "0")
            {
                ViewBag.message = "Thanh toán thất bại!";
                return View();
            }
            else
            {
                ViewBag.message = "Thanh toán thành công!";
                return View();
            }
        }

        [HttpPost]
        public ActionResult notifyurl()
        {
            string json;
            using (var reader = new StreamReader(HttpContext.Request.InputStream))
            {
                json = reader.ReadToEnd();
            }
            string last = json.Substring(json.IndexOf("errorCode") - 2);
            string[] emp = last.Split('&');
            string param = json.Substring(0, json.IndexOf("errorCode") - 1);
            param += "&" + emp[2] + "&" + emp[3] + "&" + emp[4] + "&" + emp[1] + "&" + emp[7] + "&" + emp[6];
            param = Server.UrlDecode(param);
            MoMo.MoMoSecurity crypto = new MoMo.MoMoSecurity();
            string secretKey = ConfigurationManager.AppSettings["secretKey"].ToString();
            string signatrue = crypto.signSHA256(param, secretKey);
            if (signatrue != emp[5].Substring(10))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NonAuthoritativeInformation);
            }
            if (emp[1].Substring(10) != "0")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                emp[6] = emp[6].Substring(10);
                emp[6] = Server.UrlDecode(emp[6]);
                string[] details = emp[6].Split(';');
                HoaDon hd = new HoaDon();
                hd.DiaChi = details[2];
                hd.MaThe = 9;
                hd.MaTK = long.Parse(details[1]);
                hd.NgayMua = DateTime.Now;
                hd.TrangThai = 1;
                hd.TongTien = 0;
                hd.SDTKH = details[3];
                TempData["ttt"] = emp[6];
                db.HoaDons.Add(hd);
                long dem = 4;
                for (int i = 0; i < long.Parse(details[0]); i++)
                {
                    CT_HoaDon cthd = new CT_HoaDon();
                    cthd.MaCL = long.Parse(details[dem]);
                    dem++;
                    cthd.MaMau = long.Parse(details[dem]);
                    dem++;
                    cthd.MaSize = int.Parse(details[dem]);
                    dem++;
                    cthd.MaSP = long.Parse(details[dem]);
                    dem++;
                    cthd.SoLuong = long.Parse(details[dem]);
                    dem++;
                    cthd.ThanhTien = long.Parse(details[dem]);
                    dem++;
                    db.GioHangs.Remove(db.GioHangs.Find(long.Parse(details[1]), cthd.MaSP, cthd.MaSize, cthd.MaMau));
                    db.CT_HoaDon.Add(cthd);
                }
                db.SaveChanges();
                Session["Cart"] = null;
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            //return null;
        }
        public ActionResult DoiThongTin(string TenKH, string DiaChi, string Email, string sdt)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            var tk = db.TaiKhoans.Find(long.Parse(Session["user_name"].ToString()));
            tk.TenNguoiDung = TenKH;
            tk.DiaChi = DiaChi;
            tk.Email = Email;
            tk.SDT = sdt;
            db.Entry(tk).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Tus = "Cập nhật thông tin thành công!";
            tk = db.TaiKhoans.Find(long.Parse(Session["user_name"].ToString()));
            return PartialView("_ThongTinKH", tk);
        }


    }
}
