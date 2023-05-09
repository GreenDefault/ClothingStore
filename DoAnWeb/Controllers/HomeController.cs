using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;
using System.Web.Mvc;
using System.Data.Entity;
using DoAnWeb.ViewModels;

namespace DoAnWeb.Controllers
{
    public class HomeController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();
        public ActionResult Index()
        {
            //SPnSL sPnSL = new SPnSL();
            //List<cn> cnns = new List<cn>();
            //List<Thing> things = new List<Thing>();
            //List<SanPham> sanPhams = new List<SanPham>();
            //DateTime dt = DateTime.Today.AddMonths(-1);
            //var ban = db.CT_HoaDon.Include(c => c.ChatLieu).Include(c => c.HoaDon).Include(c => c.Mau).Include(c => c.SanPham).Include(c => c.Size).OrderByDescending(o => o.HoaDon.NgayMua > dt).ToList();
            //if (ban.Count <= 0)
            //{
            //    var hang = db.SanPhams.OrderByDescending(m=>m.MaSP).ToList();
            //    foreach(var item in hang)
            //    {
            //        cn cnn = new cn();
            //        cnn.sanPham = item;
            //        var sls = db.SoLuongs.Where(m => m.MaSP.Equals(item.MaSP)).Where(m => m.SoLuong1 > 0).ToList();
            //        foreach (var temi in sls)
            //        {
            //            AnhSP anhSP = db.AnhSPs.Find(temi.MaSP, temi.MaMau);
            //            Thing thing = new Thing(temi.Size.TenSize, temi.Mau.TenMau, anhSP.Anh, long.Parse(temi.SoLuong1.ToString()), temi.MaMau);
            //            cnn.things.Add(thing);
            //        }
            //        cnns.Add(cnn);
            //    }
            //    sPnSL.spNsl = cnns;
            //    return View(sPnSL);
            //}
            //int a = 0;
            //Dictionary<long, long> check = new Dictionary<long, long>();
            //foreach(var item in ban)
            //{
            //    if (a == 10)
            //    {
            //        break;
            //    }
            //    if(check.ContainsKey(item.MaSP))
            //    {
            //        check[item.MaSP] += long.Parse(item.SoLuong.ToString());
            //    }
            //    else
            //    {
            //        check.Add(item.MaSP, long.Parse(item.SoLuong.ToString()));
            //        a++;
            //    }
            //}
            //check.OrderBy(key => key.Value);
            //foreach (var item in check)
            //{
            //    SanPham sanPham = db.SanPhams.Find(item.Key);
            //    cn cnn = new cn();
            //    cnn.sanPham = sanPham;
            //    var sls = db.SoLuongs.Where(m => m.MaSP.Equals(item.Key)).Where(m => m.SoLuong1 > 0).ToList();
            //    foreach(var temi in sls)
            //    {
            //        AnhSP anhSP = db.AnhSPs.Find(temi.MaSP, temi.MaMau);
            //        Thing thing = new Thing(temi.Size.TenSize, temi.Mau.TenMau, anhSP.Anh, long.Parse(temi.SoLuong1.ToString()), temi.MaMau);
            //        cnn.things.Add(thing);
            //    }
            //    cnns.Add(cnn);
            //}
            //sPnSL.spNsl = cnns;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ChiTiet(long? id)
        {
            return RedirectToAction("Details", "SanPhamKHs", new {id = id });
        }
    }
}