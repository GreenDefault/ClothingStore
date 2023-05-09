using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index(int? id)
        {
            return RedirectToAction("Index", "AnSPnSL", new { id = id });
        }
    }
}