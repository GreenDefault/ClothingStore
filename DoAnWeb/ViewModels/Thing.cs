using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.ViewModels
{
    public class Thing
    {
        public string Size { get; set; }
        public string AnhMau { get; set; }
        public string AnhSP { get; set; }
        public long SLuong { get; set; }
        public long Mamau { get; set; }
        public Thing(string TenSize, string AnhMau, string AnhSp, long SoLuong, long MaMau)
        {
            this.Size = TenSize + " ";
            this.AnhMau = AnhMau;
            this.AnhSP = AnhSp;
            this.SLuong = SoLuong;
            this.Mamau = MaMau;
        }
        public Thing ()
        {
            this.Size = null;
            this.AnhMau = null;
            this.AnhSP = null;
            this.SLuong = 0;
        }
    }
}