﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UrunSatisV21Entities : DbContext
    {
        public UrunSatisV21Entities()
            : base("name=UrunSatisV21Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Sepet> Sepet { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<Uye> Uye { get; set; }
        public virtual DbSet<Yorum> Yorum { get; set; }
        public virtual DbSet<Begen> Begen { get; set; }
    }
}
