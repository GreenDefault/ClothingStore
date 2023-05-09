using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.Areas.Admin.ViewModels
{
    public class losAngeles
    {
        public SanPham sp { get; set; }
        public List<Tuple<long, int, long, string>> anh { get; set; }
        public losAngeles()
        {
            sp = new SanPham();
            anh = null;
        }
    }
}