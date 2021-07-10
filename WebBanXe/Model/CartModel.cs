using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanXe.Model
{
    public class CartModel
    {
        public List<ProductModel> ListProduct { get; set; }
        public int DiscountAmount { get; set; }
        public int IdDiscount { get; set; }
        public string DiscountCode { get; set; }

        public CartModel()
        {
            ListProduct = new List<ProductModel>();
            DiscountCode = string.Empty;
        }

        public void Clear()
        {
            ListProduct.Clear();
            DiscountAmount = 0;
            IdDiscount = 0;
            DiscountCode = string.Empty;
        }
    }
}