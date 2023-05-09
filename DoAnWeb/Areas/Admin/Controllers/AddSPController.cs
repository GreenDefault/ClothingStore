using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;
using DoAnWeb.Areas.Admin.ViewModels;
using System.IO;

namespace DoAnWeb.Areas.Admin.Controllers
{
    public class AddSPController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();
        // GET: Admin/AddSP
        public ActionResult Create()
        {
            ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu");
            ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC");
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH");
            ViewBag.MaMau0 = new SelectList(db.Maus, "MaMau", "TenMau");
            ViewBag.Size0 = new SelectList(db.Sizes, "MaSize", "TenSize");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection forms)
        {
            if (ModelState.IsValid)
            {
                //HttpPostedFileBase Anh = Request.Files["Anh"];
                SanPham sanpham = new SanPham();
                List<Size> sizes = db.Sizes.ToList();
                sanpham.TenSP = forms[1];
                sanpham.DonGia = long.Parse(forms[2]);
                sanpham.MaLoaiSP = long.Parse(forms[3]);
                sanpham.MaCL = long.Parse(forms[4]);
                sanpham.MaNCC = long.Parse(forms[5]);
                sanpham.MaTH = long.Parse(forms[6]);
                sanpham.MaGT = int.Parse(forms[7]);
                db.SanPhams.Add(sanpham);
                int k = 0;
                for(int i = 8; i<forms.Count - 1; i=i+sizes.Count+1)
                {
                    List<long> check = new List<long>();
                    string anh = "Anh" + k;
                    HttpPostedFileBase Anh = Request.Files[anh];
                    if(Anh!=null)
                    {
                        if (!check.Contains(long.Parse(forms[i])))
                        {
                            AnhSP anhSP = new AnhSP();
                            string path = Path.Combine(Server.MapPath("~/Assets/Images "), Path.GetFileName(Anh.FileName));
                            Anh.SaveAs(path);
                            anhSP.MaMau = long.Parse(forms[i]);
                            anhSP.Anh = Anh.FileName;
                            db.AnhSPs.Add(anhSP);
                        }
                        else
                        {
                            ViewBag.Error = "Trùng Màu!";
                            return View("Create");
                        }
                        
                    }
                    int j = 1;
                    foreach(var item in sizes)
                    {
                        SoLuong soLuong = new SoLuong();
                        soLuong.MaMau = long.Parse(forms[i]);
                        soLuong.MaSize = item.MaSize;
                        if (forms[i + j] == "")
                        {
                            j++;
                            continue;
                        }
                        soLuong.SoLuong1 = long.Parse(forms[i + j]);
                        db.SoLuongs.Add(soLuong);
                        j++;
                    }
                    k++;
                }
                db.SaveChanges();
                return RedirectToAction("Index", "SanPhams");
            }

            //ViewBag.MaCL = new SelectList(db.ChatLieux, "MaCL", "TenChatLieu", SanPham.Sp.MaCL);
            //ViewBag.MaGT = new SelectList(db.GioiTinhs, "MaGT", "GioiTinh1", SanPham.Sp.MaGT);
            //ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", SanPham.Sp.MaLoaiSP);
            //ViewBag.MaNCC = new SelectList(db.NCCs, "MaNCC", "TenNCC", SanPham.Sp.MaNCC);
            //ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", SanPham.Sp.MaTH);
            //ViewBag.Mau = new SelectList(db.Maus, "MaMau", "TenMau", SanPham.Sl.MaMau);
            //ViewBag.Size = new SelectList(db.Sizes, "MaSize", "TenSize", SanPham.Sl.MaSize);
            return View();
        }
    }
}