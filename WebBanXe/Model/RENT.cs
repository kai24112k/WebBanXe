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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class RENT
    {
        public int IdRent { get; set; }
        [Display(Name = "NGÀY THUÊ")]
        public System.DateTime DateRent { get; set; }
        [Display(Name = "NGÀY TRẢ")]
        public System.DateTime DateBack { get; set; }
        [Display(Name = "GIÁ")]
        public int Price { get; set; }
        [Display(Name = "TRẠNG THÁI")]
        public bool Status { get; set; }
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
    
        public virtual PRODUCT PRODUCT { get; set; }
        public virtual USER USER { get; set; }
    }
}
