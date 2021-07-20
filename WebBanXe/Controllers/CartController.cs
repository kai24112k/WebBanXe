using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Helpers.Email;
using WebBanXe.Helpers.Momo;
using WebBanXe.Model;
using static WebBanXe.Helpers.Security.Authen;

namespace WebBanXe.Controllers
{
    //[Auth(new int[] { (int)Role.Admin })]
    [Auth]
    public class CartController : Controller
    {
   
        // GET: Cart
        DBBanXeEntities db = new DBBanXeEntities();
        //Lay gio hang
        public CartModel GetCart()
        {
            CartModel listCart = Session["Giohang"] as CartModel;
            if (listCart == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                listCart = new CartModel();
                Session["Giohang"] = listCart;
            }
            return listCart;
        }
        public ActionResult AddCart(int iMaProduct, string strURL)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            //Lay ra Session gio hang
            var listProduct = GetCart();
            //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
            ProductModel product = listProduct.ListProduct.Find(n => n.iMaProduct == iMaProduct);
            if (product == null)
            {
                product = new ProductModel(iMaProduct);
                listProduct.ListProduct.Add(product);
                return Redirect(strURL);
            }
            else
            {
                product.iQuantity++;
                return Redirect(strURL);
            }
        }

        //Xay dung trang Gio hang
        [Route("gio-hang")]
        public ActionResult Cart()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            var listCart = GetCart();
            if (listCart.ListProduct.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.FinalMoney = FinalMoney();

            if (TempData["Error"] != null) ViewBag.Error = TempData["Error"].ToString();

            return View(listCart);
        }


        //Tong so luong
        private int TotalQuantity()
        {
            int iTotalQuantity = 0;
            var listCart = GetCart();
            if (listCart != null)
            {
                iTotalQuantity = listCart.ListProduct.Sum(n => n.iQuantity);
            }
            return iTotalQuantity;
        }
        //Tinh tong tien
        private double FinalMoney()
        {
            int finalMoney = 0;
            var cart = GetCart();
            if (cart != null)
            {
                finalMoney = cart.ListProduct.Sum(n => n.sTotalMoney);
            }

            finalMoney -= cart.DiscountAmount;

            return finalMoney;
        }
        //Tao Partial view de hien thi thong tin gio hang
        public ActionResult CartPartial()
        {
            ViewBag.TotalQuantity = TotalQuantity();
            ViewBag.FinalMoney = FinalMoney();
            return PartialView();
        }

        //Cap nhat Giỏ hàng
        [Route("cap-nhat-gio-hang")]
        public ActionResult UpdateCart(FormCollection f)
        {

            //Lay gio hang tu Session
            var listCart = GetCart();
            string[] quantity = f.GetValues("txtSoluong");
            //Kiem tra sach da co trong Session["Giohang"]
            for (int i = 0; i < listCart.ListProduct.Count(); i++)
            {
                listCart.ListProduct[i].iQuantity = int.Parse(quantity[i]);
            }

            //Neu ton tai thi cho sua Soluong
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteCart(int iMaProduct)
        {
            //Lay gio hang tu Session
            var listCart = GetCart();
            //Kiem tra sach da co trong Session["Giohang"]
            ProductModel product = listCart.ListProduct.SingleOrDefault(n => n.iMaProduct == iMaProduct);
            //Neu ton tai thi cho sua Soluong
            if (product != null)
            {
                listCart.ListProduct.RemoveAll(n => n.iMaProduct == iMaProduct);
                return RedirectToAction("Cart");

            }
            if (listCart.ListProduct.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Cart");
        }
        //Xoa tat ca thong tin trong Gio hang

        [Route("xoa-gio-hang")]
        public ActionResult DeleteAllCart()
        {
            //Lay gio hang tu Session
            var listCart = GetCart();
            listCart.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Discount(FormCollection f)
        {
            if (f["txtDiscount"] is null)
            {
                return RedirectToAction("Cart");
            }

            string code = f["txtDiscount"].ToString();
            DISCOUNT discount = new DISCOUNT();
            discount = db.DISCOUNTs.Where(d => d.CodeDiscount.ToLower() == code.ToLower()).FirstOrDefault();
            if (discount is null)
            {
                TempData["Error"] = "Không tìm thấy mã giảm giá này";
            }
            else
            {
                var cart = GetCart();
                cart.IdDiscount = discount.IdDiscount;
                cart.DiscountAmount = discount.Value;
                cart.DiscountCode = code;

                Session["Giohang"] = cart;
            }


            return RedirectToAction("Cart");
        }

        /// <summary>
        /// Đặt hàng
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("dat-hang")]
        public ActionResult OrderCart(FormCollection f)
        {
            // Lấy hình thức thanh toán
            // 0 - thanh toán online/ 1 - thanh toán COD
            var formObj = f["hinhthucthanhtoan"];
            var paymentMethod = formObj == null ? "0" : formObj.ToString();

            // Get cart
            var cart = GetCart();

            // Define id order
            int idOrder = 0;

            // Save order
            try
            {
                using (DBBanXeEntities db = new DBBanXeEntities())
                {
                    ORDER order = new ORDER();
                    order.DayCreate = DateTime.Now;
                    order.IdUser = int.Parse(Session["userID"].ToString());

                    // Status default is 0 - havent payment yet
                    order.Status = 0;

                    // Discount
                    order.ReduceMoney = 0;
                    if (cart.IdDiscount > 0)
                    {
                        var discountDB = db.DISCOUNTs.Find(cart.IdDiscount);
                        if (discountDB is null) throw new Exception("Mã giảm giá không tồn tại");
                        if (discountDB.Status == false) throw new Exception("Mã giảm giá không khả dụng");
                        order.IdDiscount = cart.IdDiscount;
                        discountDB.Status = false;
                        order.ReduceMoney = discountDB.Value;
                    }

                    // Tính tiền
                    order.TotalMoney = cart.ListProduct.Sum(x => x.sTotalMoney);
                    order.FinalMoney = order.TotalMoney - order.ReduceMoney;

                    // Add product detail
                    if (cart.ListProduct is null || cart.ListProduct.Count < 1) throw new Exception("Vui lòng thêm ít nhất một sản phẩm");

                    // Add order vào db
                    db.ORDERs.Add(order);
                    db.SaveChanges();

                    // Get id order
                    idOrder = order.IdOrder;
                    foreach (var product in cart.ListProduct)
                    {
                        ORDER_DETAIL orderDetail = new ORDER_DETAIL();
                        orderDetail.IdOrder = idOrder;
                        orderDetail.IdProduct = product.iMaProduct;
                        orderDetail.Price = product.sPrice;
                        orderDetail.Quantity = product.iQuantity;
                        order.ORDER_DETAIL.Add(orderDetail);
                    }

                    // Order success
                    try
                    {
                        int idUser = int.Parse(Session["userID"].ToString());
                        var userDB = db.USERs.Find(idUser);
                        if (userDB != null && !string.IsNullOrEmpty(userDB.Email))
                        {
                            var paymentName = paymentMethod.Equals("0") ? "MOMO" : "COD";
                            var body = $@"Kính chào anh/chị <b>{userDB.FullName}</b>,<br><br>
                        Đơn hàng số {idOrder} đã được đặt thành công.<br>
                        Phương thức thanh toán: {paymentName}<br><br>
                        Cám ơn anh chị đã đặt hàng.
                        ";

                            EmailHelper.SendMail(userDB.Email, "ĐẶT HÀNG THÀNH CÔNG ĐƠN HÀNG SỐ " + idOrder, body);
                        }
                    }
                    catch
                    {
                        // Send mail là optional nên nếu error thì bỏ qua
                    }

                }

                // Add payment method
                if (paymentMethod.Equals("0"))
                {
                    // Add momo
                    string urlPaymentMomo = MomoHelper.CreateLinkPayment(idOrder);
                    if (string.IsNullOrEmpty(urlPaymentMomo)) throw new Exception("Không thanh toán được qua momo, đã chuyển hình thức thanh toán COD");

                    // Clear cart
                    Session.Remove("Giohang");

                    return Redirect(urlPaymentMomo);
                }
                else
                {
                    // Clear cart
                    Session.Remove("Giohang");
                }

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Cart");
            }

            ViewBag.Success = $"Đặt hàng thành công, đơn hàng của bạn có mã số là {idOrder}, chúng tôi sẽ liên lạc với bạn trong thời gian sớm nhất";
            return View();
        }


        [Route("thanh-toan-momo/{id}")]
        public ActionResult PaymentAgain(int id)
        {
            using (DBBanXeEntities db = new DBBanXeEntities())
            {
                var orderDB = db.ORDERs.Find(id);
                if (orderDB is null) return RedirectToAction("Index", "Home");
                if (orderDB.Status == 1) return RedirectToAction("Index", "Home");
            }

            string urlPaymentMomo = MomoHelper.CreateLinkPayment(id);
            if (string.IsNullOrEmpty(urlPaymentMomo)) return RedirectToAction("Index", "Home");
            return Redirect(urlPaymentMomo);
        }

        /// <summary>
        /// Trang hiển thị thanh toán thành công
        /// </summary>
        /// <returns></returns>
        [Route("thanh-toan-thanh-cong")]
        public ActionResult PaymentSuccess()
        {
            try
            {
                if (Request.QueryString["orderInfo"] is null) throw new Exception("Không đúng định dạng url");
                var orderInfoText = Request.QueryString["orderInfo"].ToString();
                var orderIDText = string.Empty;
                foreach (var c in orderInfoText)
                {
                    if (char.IsDigit(c)) orderIDText += c;
                }
                var orderID = int.Parse(orderIDText);

                if (MomoHelper.CheckPaymentSuccess(orderID))
                {
                    // Check thanh toán thành công
                    ViewBag.Title = "Xác nhận thanh toán thành công";
                    ViewBag.Success = $"Thanh toán thành công, đơn hàng của bạn có mã số là {orderID}, chúng tôi sẽ liên lạc với bạn trong thời gian sớm nhất";

                    // Send mail là optional
                    try
                    {
                        int idUser = int.Parse(Session["userID"].ToString());
                        var userDB = db.USERs.Find(idUser);
                        if (userDB != null && !string.IsNullOrEmpty(userDB.Email))
                        {
                            var body = $@"Kính chào anh/chị <b>{userDB.FullName}</b>,<br><br>
                        Đơn hàng số {orderID} đã được thanh toán thành công.<br>
                        Chúng tôi sẽ liên hệ với bạn để giao hàng trong thời gian sớm nhất<br><br>
                        Cám ơn anh chị đã đặt hàng.
                        ";

                            EmailHelper.SendMail(userDB.Email, "THANH TOÁN THÀNH CÔNG ĐƠN HÀNG SỐ " + orderID, body);
                        }
                    }
                    catch
                    {
                        // Ko làm gì cả vì send mail là optional
                    }

                }
                else
                {
                    // Thanh toán thất bại
                    ViewBag.Title = "Thanh toán chưa hoàn tất";
                    ViewBag.Error = "Đơn hàng này chưa thanh toán thành công, nếu bạn đã thanh toán, xin vui lòng liên hệ với chúng tôi để kiểm tra biên nhận";
                }


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View("OrderCart");
        }
    }
}