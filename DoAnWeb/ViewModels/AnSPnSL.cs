using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;
using DoAnWeb.ViewModels;


namespace DoAnWeb.ViewModels
{
    public class AnSPnSL
    {
        public List<ASPSL> ASPSLs { get; set; }
        public List<LoaiSP> Loais { get; set; }
        public List<ThuongHieu> ThuongHieus { get; set; }
        public List<ChatLieu> chatLieus { get; set; }
    }
}