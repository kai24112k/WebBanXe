using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Model;

namespace WebBanXe.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DBBanXeEntities db = new DBBanXeEntities();
        //Lay gio hang
        public List<Cart> getCart()
        {
            List<Cart> listCart = Session["Giohang"] as List<Cart>;
            if (listCart == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                listCart = new List<Cart>();
                Session["Giohang"] = listCart;
            }
            return listCart;
        }
        public ActionResult addCart(int iMaProduct, string strURL)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            //Lay ra Session gio hang
            List<Cart> listProduct = getCart();
            //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
            Cart product = listProduct.Find(n => n.iMaProduct == iMaProduct);
            if (product == null)
            {
                product = new Cart(iMaProduct);
                listProduct.Add(product);
                return Redirect(strURL);
            }
            else
            {
                product.iQuantity++;
                return Redirect(strURL);
            }
        }

        //Xay dung trang Gio hang
        public ActionResult Cart()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }   
            List<Cart> listCart = getCart();
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.FinalMoney = FinalMoney();
            return View(listCart);
        }
        //Tong so luong
        private int TotalQuantity()
        {
            int iTotalQuantity = 0;
            List<Cart> listCart = Session["GioHang"] as List<Cart>;
            if (listCart != null)
            {
                iTotalQuantity = listCart.Sum(n => n.iQuantity);
            }
            return iTotalQuantity;
        }
        //Tinh tong tien
        private double FinalMoney()
        {
            int iFinalMoney = 0;
            List<Cart> listCart = Session["GioHang"] as List<Cart>;
            if (listCart != null)
            {
                iFinalMoney = listCart.Sum(n => n.sTotalMoney);
            }
            return iFinalMoney;
        }
        //Tao Partial view de hien thi thong tin gio hang
        public ActionResult CartPartial()
        {
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.FinalMoney = FinalMoney();
            return PartialView();
        }
        //Cap nhat Giỏ hàng
        public ActionResult UpdateCart(FormCollection f)
        {

            //Lay gio hang tu Session
            List<Cart> listCart = getCart();
            string[] quantity = f.GetValues("txtSoluong");
            //Kiem tra sach da co trong Session["Giohang"]
            for(int i=0;i< listCart.Count(); i++)
            {
                listCart[i].iQuantity = int.Parse(quantity[i]);
            }
           
            //Neu ton tai thi cho sua Soluong
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteCart(int iMaProduct)
        {
            //Lay gio hang tu Session
            List<Cart> listCart = getCart();
            //Kiem tra sach da co trong Session["Giohang"]
            Cart product = listCart.SingleOrDefault(n => n.iMaProduct == iMaProduct);
            //Neu ton tai thi cho sua Soluong
            if (product != null)
            {
                listCart.RemoveAll(n => n.iMaProduct == iMaProduct);
                return RedirectToAction("Cart");

            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult DeleteAllCart()
        {
            //Lay gio hang tu Session
            List<Cart> listCart = getCart();
            listCart.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}