using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.ViewModels;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.ViewModels
{
    public class ctSPnCMT
    {
        public ctSP sanpham { get; set; }
        public List<Comment> cmt { get; set; }
        public ctSPnCMT()
        {
            this.sanpham = new ctSP();
            this.cmt = new List<Comment>();
        }
    }
}