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
    
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            this.RepCMTs = new HashSet<RepCMT>();
        }
    
        public long MaCMT { get; set; }
        public Nullable<long> MaTK { get; set; }
        public long MaSP { get; set; }
        public Nullable<int> DanhGia { get; set; }
        public string CMT { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual SanPham SanPham { get; set; }
        public virtual TaiKhoan TaiKhoan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RepCMT> RepCMTs { get; set; }
    }
}
