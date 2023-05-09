using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;

namespace DoAnWeb.ViewModels
{
    public class SPnSL
    {
        public ICollection<cn> spNsl { get; set; }
        public string anhh { get; set; }
        public SPnSL()
        {
            spNsl = null;
            anhh = null;
        }
    }
}