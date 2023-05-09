using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.ViewModels;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.ViewModels
{
    public class cn
    {
        public List<Thing> things { get; set; }
        public SanPham sanPham { get; set; }
        public cn(List<Thing> a, SanPham d)
        {
            this.things = a;
            this.sanPham = d;
        }
        public cn()
        {
            this.things = new List<Thing>();
            this.sanPham = new SanPham();
        }
    }
}