using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.ViewModels;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.ViewModels
{
    public class ctSP
    {
        public ASPSL sanp { get; set; }
        public AnSPnSL lqs { get; set; }
        public ctSP()
        {
            sanp = new ASPSL();
            lqs = new AnSPnSL();
        }
    }
}