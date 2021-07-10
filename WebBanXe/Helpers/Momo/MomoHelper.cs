using MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanXe.Model;

namespace WebBanXe.Helpers.Momo
{
    public class MomoHelper
    {
        /// <summary>
        /// Tạo một link thanh toán
        /// </summary>
        /// <param name="money"></param>
        /// <param name="idOrder"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string CreateLinkPayment(int idOrder)
        {
            int money = 0;
            string content = string.Empty;
            using (DBBanXeEntities db = new DBBanXeEntities())
            {
                var orderDB = db.ORDERs.Find(idOrder);
                if (orderDB is null) throw new Exception("Không tìm thấy đơn hàng có mã là " + idOrder);
                money = orderDB.FinalMoney;
                content = "Thanh toan cho don hang " + idOrder;
            }

            string endpoint = MomoSetting.ApiEndpoint;
            string partnerCode = MomoSetting.PartnerCode;
            string accessKey = MomoSetting.AccessKey;
            string serectkey = MomoSetting.SecretKey;
            string orderInfo = content;
            string returnUrl = "https://" + HttpContext.Current.Request.Url.Authority + "/thanh-toan-thanh-cong";
            string notifyurl = "https://momo.vn/return";

            string amount = money.ToString();
            string momoOrderID = Guid.NewGuid().ToString();
            string orderid = momoOrderID;
            string requestId = momoOrderID;
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            // Lưu order id momo vào đơn hàng đó
            using (DBBanXeEntities db = new DBBanXeEntities())
            {
                var orderDB = db.ORDERs.Find(idOrder);
                if (orderDB is null) throw new Exception("Không tìm thấy đơn hàng có mã là " + idOrder);
                orderDB.MomoPaymentID = momoOrderID;
                db.SaveChanges();
            }

            return jmessage.GetValue("payUrl").ToString();
        }

        /// <summary>
        /// Hàm kiểm tra thanh toán
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public static bool CheckPaymentSuccess(int idOrder)
        {
            string momoPaymentID = string.Empty;
            using (DBBanXeEntities db = new DBBanXeEntities())
            {
                var orderDB = db.ORDERs.Find(idOrder);
                if (orderDB is null) throw new Exception("Không tìm thấy đơn hàng có mã là " + idOrder);
                momoPaymentID = orderDB.MomoPaymentID;
            }

            string endpoint = MomoSetting.ApiEndpoint;
            string partnerCode = MomoSetting.PartnerCode;
            string accessKey = MomoSetting.AccessKey;
            string serectkey = MomoSetting.SecretKey;

            string orderid = momoPaymentID;
            string requestId = momoPaymentID;

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&orderId=" +
                orderid + "&requestType=transactionStatus";

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "orderId", orderid },
                { "requestType", "transactionStatus" },
                { "signature", signature }

            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            // Update vao db
            using (DBBanXeEntities db = new DBBanXeEntities())
            {
                var orderDB = db.ORDERs.Find(idOrder);
                if (orderDB is null) throw new Exception("Không tìm thấy đơn hàng có mã là " + idOrder);
                if (jmessage.GetValue("errorCode").ToString().Equals("0"))
                {
                    orderDB.Status = 1;
                    db.SaveChanges();
                }
            }

            return jmessage.GetValue("errorCode").ToString().Equals("0");
        }
    }
}