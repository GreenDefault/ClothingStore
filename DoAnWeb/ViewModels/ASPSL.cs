using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.ViewModels
{
    public class ASPSL
    {
        public List<AnhSP> Anhh { get; set; }
        public SanPham Sanss { get; set; }
        public List<SoLuong> Luongs { get; set; }
        public ASPSL() 
        {
            this.Sanss = new SanPham();
            this.Luongs = new List<SoLuong>();
            this.Anhh = new List<AnhSP>();
        }
    }

}