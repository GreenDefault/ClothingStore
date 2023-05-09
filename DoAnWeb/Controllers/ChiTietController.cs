using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class ChiTietController : Controller
    {
        // GET: ChiTiet
        public ActionResult ChiTiet(long? id)
        {
            return RedirectToAction("Details", "SanPhamKHs", new { id = id });
        }
        public ActionResult abc()
        {
            return View();
        }
    }
}