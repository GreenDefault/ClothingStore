//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnWeb.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GioHang
    {
        public long MaTK { get; set; }
        public long MaSP { get; set; }
        public Nullable<long> MaCL { get; set; }
        public int MaSize { get; set; }
        public long MaMau { get; set; }
        public Nullable<long> SoLuong { get; set; }
        public Nullable<long> ThanhTien { get; set; }
    
        public virtual ChatLieu ChatLieu { get; set; }
        public virtual Mau Mau { get; set; }
        public virtual SanPham SanPham { get; set; }
        public virtual Size Size { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
