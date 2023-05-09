using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;
namespace DoAnWeb.ViewModels
{
    public class onlyreason
    {
        public GioHang gh { get; set; }
        public string anh { get; set; }
        public onlyreason()
        {
            gh = null;
            anh = null;
        }
    }
}