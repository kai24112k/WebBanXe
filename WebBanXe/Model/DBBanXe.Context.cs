﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanXe.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBBanXeEntities : DbContext
    {
        public DBBanXeEntities()
            : base("name=DBBanXeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BLOG> BLOGs { get; set; }
        public virtual DbSet<BRAND> BRANDs { get; set; }
        public virtual DbSet<CATEGORY_BLOG> CATEGORY_BLOG { get; set; }
        public virtual DbSet<CLASS> CLASSes { get; set; }
        public virtual DbSet<CONTACT> CONTACTs { get; set; }
        public virtual DbSet<DISCOUNT> DISCOUNTs { get; set; }
        public virtual DbSet<IMAGE_CONTENT> IMAGE_CONTENT { get; set; }
        public virtual DbSet<IMG_BLOG> IMG_BLOG { get; set; }
        public virtual DbSet<IMG_PRODUCT> IMG_PRODUCT { get; set; }
        public virtual DbSet<ORDER> ORDERs { get; set; }
        public virtual DbSet<ORDER_DETAIL> ORDER_DETAIL { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<RENT> RENTs { get; set; }
        public virtual DbSet<TYPECAR> TYPECARs { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
        public virtual DbSet<USER_ROLE> USER_ROLE { get; set; }
    }
}