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

    public partial class CONTACT
    {
        public int IdContact { get; set; }
        public Nullable<int> IdUser { get; set; }
        [Display(Name = "TIÊU ĐỀ")]
        public string Title { get; set; }
        [Display(Name = "EMAIL")]
        public string Email { get; set; }
        [Display(Name = "NỘI DUNG")]
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Content { get; set; }
        [Display(Name = "TRẠNG THÁI")]
        public Nullable<bool> Status { get; set; }
    
        public virtual USER USER { get; set; }
    }
}
