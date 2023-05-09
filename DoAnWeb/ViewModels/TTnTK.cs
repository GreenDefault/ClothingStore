using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.ViewModels
{
    public class TTnTK
    {
        public IEnumerable<TrangThai> tt { get; set; }
        public  TaiKhoan tk { get; set; }
    }
}