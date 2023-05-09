using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.ViewModels;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.Areas.Admin.ViewModels
{
    public class SPnSL_Admin
    {
        public SoLuong Sl { get; set; }
        public SanPham Sp { get; set; }
        public string Anh0 { get; set; }
    }
}