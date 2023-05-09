using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Areas.Admin.Models;
using DoAnWeb.ViewModels;

namespace DoAnWeb.Controllers
{
    public class SPnSLController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();
        // GET: SPnSL
        public ActionResult Index()
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsoluteUri;
                if (Request.Headers["X-Forwarded-Proto"] != null)
                {
                    rrr = rrr.Insert(4, Request.Headers["X-Forwarded-Proto"]);
                }
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            long k = long.Parse(Session["user_name"].ToString());
            var sanphams = db.SanPhams.ToList();
            //var last = db.SanPhams.Last();
            //for(int i =0; i<last.MaSP; i++)
            //{
            //    SanPham pham = db.SanPhams.Find(i);
            //}
            //var size = db.Sizes;
            SPnSL sp = new SPnSL();
            List<cn> cns = new List<cn>();
            foreach (var c in sanphams)
            {
                cn ca = new cn();
                ca.sanPham = c;
                List<SoLuong> sll = new List<SoLuong>();
                //sp.b = db.SoLuongs.Where(g=> g.MaSP.Equals(long.Parse(c.MaSP.ToString()))).Where(w => w.SoLuong1.Value.Equals(int.Parse("0")))
                long kkk = long.Parse(c.MaSP.ToString());
                sll = db.SoLuongs.Where(g => g.MaSP.Equals(kkk)).Where(w => w.SoLuong1 > 0).ToList();
                List<Thing> qqq = new List<Thing>();
                List<int> checkSize = new List<int>();
                List<long> checkMau = new List<long>();
                foreach (var q in sll)
                {
                    Thing thing = new Thing(q.Size.TenSize, q.Mau.Anh, db.AnhSPs.Find(q.MaSP,q.MaMau).Anh, long.Parse(q.SoLuong1.ToString()), q.MaMau);
                    checkSize.Add(q.MaSize);
                    checkMau.Add(q.MaMau);
                    qqq.Add(thing);
                }
                ca.things = qqq;
                cns.Add(ca);
                
            }
            sp.spNsl = cns;
            return View(sp);
        }
    }
}