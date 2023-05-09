using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoAnWeb.Areas.Admin.Models;
namespace DoAnWeb.ViewModels
{
    public class ViewModel_TTnGH
    {
        public IEnumerable<ThanhToanOnline> TTs { get; set; }
        public IEnumerable<GioHang> GHs { get; set; }
    }
}