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
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.AnhSPs = new HashSet<AnhSP>();
            this.Comments = new HashSet<Comment>();
            this.CT_HoaDon = new HashSet<CT_HoaDon>();
            this.GioHangs = new HashSet<GioHang>();
            this.SoLuongs = new HashSet<SoLuong>();
        }
    
        public long MaSP { get; set; }
        public string TenSP { get; set; }
        public Nullable<long> DonGia { get; set; }
        public Nullable<long> MaLoaiSP { get; set; }
        public Nullable<long> MaCL { get; set; }
        public Nullable<long> MaNCC { get; set; }
        public Nullable<long> MaTH { get; set; }
        public Nullable<int> MaGT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnhSP> AnhSPs { get; set; }
        public virtual ChatLieu ChatLieu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }
        public virtual GioiTinh GioiTinh { get; set; }
        public virtual LoaiSP LoaiSP { get; set; }
        public virtual NCC NCC { get; set; }
        public virtual ThuongHieu ThuongHieu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SoLuong> SoLuongs { get; set; }
    }
}
