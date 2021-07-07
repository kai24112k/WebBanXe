using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanXe.Model
{
    public class Cart
    {
        //Tao doi tuong data chua dữ liệu từ model dbBansach đã tạo. 
        DBBanXeEntities db = new DBBanXeEntities();
        public int iMaProduct { set; get; }
        public string sNameProduct { set; get; }
        public int sPrice { set; get; }
        public string sLinkImg { set; get; }
    
        public int iQuantity { set; get; }
        public int sTotalMoney
        {
            get { return iQuantity * sPrice; }

        }
        //Khoi tao gio hàng theo Masach duoc truyen vao voi Soluong mac dinh la 1
        public Cart(int IdProduct)
        {

            iMaProduct = IdProduct;
            PRODUCT product = db.PRODUCTs.Single(n => n.IdProduct == IdProduct);
            sNameProduct = product.NameProduct;
            sLinkImg = db.IMG_PRODUCT.Single(n => n.IdProduct == iMaProduct).LinkImg;
            sPrice = int.Parse(product.Price.ToString());
            iQuantity = 1;
        }
    }
}