//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UrunSatisV2API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sepet
    {
        public int sepetId { get; set; }
        public int uyeId { get; set; }
        public int urunId { get; set; }
        public Nullable<int> Fiyat { get; set; }
        public Nullable<int> Adet { get; set; }
    
        public virtual Urun Urun { get; set; }
        public virtual Uye Uye { get; set; }
    }
}
