using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.ViewModels;
using DoAnWeb.Areas.Admin.Models;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net;

namespace DoAnWeb.Controllers
{
    public class TTnGHController : Controller
    {
        // GET: VCnGH
        private DoAnWebEntities db = new DoAnWebEntities();
        public ActionResult Index()
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            long k = long.Parse(Session["user_name"].ToString());
            var tt = db.TaiKhoans.Find(k);
            ViewBag.Ten = tt.TenNguoiDung;
            ViewBag.sdt = tt.SDT;
            ViewBag.dc = tt.DiaChi;
            ViewBag.email = tt.Email;
            var gioHangs = db.GioHangs.Where(m => m.MaTK.Equals(k));
            ViewModel_TTnGH vcgh = new ViewModel_TTnGH();
            vcgh.TTs = db.ThanhToanOnlines.ToList();
            return View(vcgh);
        }
        [HttpPost]
        public ActionResult Offline(FormCollection form)
        {
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            long k = long.Parse(Session["user_name"].ToString());
            HoaDon hd = new HoaDon();
            hd.DiaChi = form["dc"];
            hd.MaThe = 7;
            hd.MaTK = long.Parse(Session["user_name"].ToString());
            hd.NgayMua = DateTime.Today;
            hd.TrangThai = 1;
            hd.SDTKH = form["sdt"];
            hd.TongTien = long.Parse(form["tong"]);
            db.HoaDons.Add(hd);
            foreach (var item in (List<Cokess>)Session["Cart"])
            {
                CT_HoaDon cthd = new CT_HoaDon();
                cthd.MaCL = item.gh.SanPham.MaCL;
                cthd.MaMau = item.gh.MaMau;
                cthd.MaSize = item.gh.MaSize;
                cthd.MaSP = item.gh.MaSP;
                cthd.SoLuong = item.gh.SoLuong;
                cthd.ThanhTien = item.gh.ThanhTien;
                db.CT_HoaDon.Add(cthd);
                db.GioHangs.Remove(db.GioHangs.Find(k, item.gh.MaSP, item.gh.MaSize, item.gh.MaMau));
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("Offline", "GioHangKHs");
        }
        public ActionResult Online(FormCollection form)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            if (Session["user_name"] == null)
            {
                string rrr = Request.Url.AbsolutePath;
                return RedirectToAction("Index", "Login", new { returnurl = rrr });
            }
            string a = form[0];
            string b = form[1];
            string c = form[2];
            string d = form[3];
            string e = form[4];
            string f = form[5];
            string endpoint = ConfigurationManager.AppSettings["endpoint"].ToString();
            string partnerCode = ConfigurationManager.AppSettings["partnerCode"].ToString();
            string accessKey = ConfigurationManager.AppSettings["accessKey"].ToString();
            string secretKey = ConfigurationManager.AppSettings["secretKey"].ToString();
            string orderInfo = "Đơn hàng của " + db.TaiKhoans.Find(long.Parse(Session["user_name"].ToString())).TenDN;
            string returnURL = ConfigurationManager.AppSettings["returnUrl"].ToString();
            string notifyURL = ConfigurationManager.AppSettings["notifyUrl"].ToString();
            string amount = form["tong"].ToString();
            string orderID = Guid.NewGuid().ToString();
            string requestID = Guid.NewGuid().ToString();
            string extraData = "";
            long detailOrder = 0;
            foreach (var item in (List<Cokess>)Session["Cart"])
            {
                detailOrder++;
                extraData += item.gh.SanPham.MaCL.ToString() + ";" + item.gh.MaMau.ToString() + ";" + item.gh.MaSize.ToString() + ";" + item.gh.MaSP.ToString() + ";" + item.gh.SoLuong.ToString() + ";" + item.gh.ThanhTien.ToString() + ";";
            }
            extraData = detailOrder.ToString() + ";" + Session["user_name"].ToString() + ";" + form["dc"].ToString() + ";" + form["std"].ToString() + ";" + extraData;

            //for endpoint="https://test-payment.momo.vn/v2/gateway/api/create"
            //string rawHash = "accessKey=" + accessKey +
            //"&amount=" + amount +
            //"&extraData=" + extraData +
            //"&ipnUrl=" + notifyURL +
            //"&orderId=" + orderID +
            //"&orderInfo=" + orderInfo +
            //"&partnerCode=" + partnerCode +
            //"&redirectUrl=" + returnURL +
            //"&requestId=" + requestID +
            //"&requestType=captureWallet";

            //JObject message = new JObject
            //{
            //    { "partnerCode", partnerCode },
            //    { "partnerName", "DinoShop" },
            //    { "storeId", "MomoTestStore" },
            //    { "requestId", requestID },
            //    { "amount", amount },
            //    { "orderId", orderID },
            //    { "orderInfo", orderInfo },
            //    { "redirectUrl", returnURL },
            //    { "ipnUrl", notifyURL },
            //    { "lang", "en" },
            //    { "extraData", extraData },
            //    { "requestType", "captureWallet" },
            //    { "signature", signature }

            //};
            string rawHash = "partnerCode=" + partnerCode +
                "&accessKey=" + accessKey +
                "&requestId=" + requestID +
                "&amount=" + amount +
                "&orderId=" + orderID +
                "&orderInfo=" + orderInfo +
                "&returnUrl=" + returnURL +
                "&notifyUrl=" + notifyURL +
                "&extraData=" + extraData;

            MoMo.MoMoSecurity crypto = new MoMo.MoMoSecurity();
            string signature = crypto.signSHA256(rawHash, secretKey);
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                {"accessKey", accessKey },
                { "requestId", requestID },
                { "amount", amount },
                { "orderId", orderID },
                { "orderInfo", orderInfo },
                {"returnUrl", returnURL },
                {"notifyUrl", notifyURL },
                {"extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }
            };
            TempData["dc"] = form["dc"];
            TempData["tong"] = form["tong"];
            string response = MoMo.PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(response);
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

        //public ActionResult ThanhToanVNPay(FormCollection form)
        //{
        //    ServicePointManager.Expect100Continue = true;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    if (Session["user_name"] == null)
        //    {
        //        string rrr = Request.Url.AbsolutePath;
        //        return RedirectToAction("Index", "Login", new { returnurl = rrr });
        //    }
        //    string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
        //    string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
        //    string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma website
        //    string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
        //    long amount = long.Parse(form["tong"]);
        //    HoaDon hoaDon = new HoaDon();
        //    hoaDon.MaThe = long.Parse(form["the"]);
        //    hoaDon.MaTK = long.Parse((Session["user_name"]).ToString());
        //    hoaDon.TongTien = 0;
        //    hoaDon.SDTKH = form["std"];
        //    hoaDon.DiaChi = form["dc"];
        //    hoaDon.TrangThai = 1;
        //    hoaDon.NgayMua = DateTime.Now;
        //    db.HoaDons.Add(hoaDon);
        //    hoaDon = db.HoaDons.LastOrDefault();
        //    VNPAY_CS_ASPX.VnPayLibrary vnpay = new VNPAY_CS_ASPX.VnPayLibrary();
        //    vnpay.AddRequestData("vnp_Version", VNPAY_CS_ASPX.VnPayLibrary.VERSION);
        //    vnpay.AddRequestData("vnp_Command", "pay");
        //    vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
        //    vnpay.AddRequestData("vnp_Amount", (amount * 100).ToString()); /*Số tiền thanh toán. Số tiền không 
        //    mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND
        //    (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần(khử phần thập phân), sau đó gửi sang VNPAY
        //   là: 10000000*/
        //    vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
        //    vnpay.AddRequestData("vnp_CurrCode", "VND");
        //    vnpay.AddRequestData("vnp_IpAddr", VNPAY_CS_ASPX.Utils.GetIpAddress());
        //    vnpay.AddRequestData("vnp_Locale", "vn");
        //    vnpay.AddRequestData("vnp_OrderInfo", "Don hang cua: " + db.TaiKhoans.Find(long.Parse(Session["user_name"].ToString())).TenDN);
        //    vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
        //    vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
        //    vnpay.AddRequestData("vnp_TxnRef", hoaDon.MaHD.ToString()); // Mã tham chiếu của giao dịch tại hệ 
        //        //Add Params of 2.1.0 Version
        //        vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));
        //    //Billing
        //    vnpay.AddRequestData("vnp_Bill_Mobile", form["std"]);
        //    vnpay.AddRequestData("vnp_Bill_Email", db.TaiKhoans.Find(hoaDon.MaTK).Email);
        //    //var fullName = txt_billing_fullname.Text.Trim();
        //    //if (!String.IsNullOrEmpty(fullName))
        //    //{
        //    //    var indexof = fullName.IndexOf(' ');
        //    //    vnpay.AddRequestData("vnp_Bill_FirstName", fullName.Substring(0, indexof));
        //    //    vnpay.AddRequestData("vnp_Bill_LastName", fullName.Substring(indexof + 1,
        //    //    fullName.Length - indexof - 1));
        //    //}
        //    vnpay.AddRequestData("vnp_Bill_Address", hoaDon.DiaChi);
        //    //vnpay.AddRequestData("vnp_Bill_City", txt_bill_city.Text.Trim());
        //    //vnpay.AddRequestData("vnp_Bill_Country", txt_bill_country.Text.Trim());
        //    //vnpay.AddRequestData("vnp_Bill_State", "");
        //    // Invoice
        //    vnpay.AddRequestData("vnp_Inv_Phone", hoaDon.SDTKH);
        //    vnpay.AddRequestData("vnp_Inv_Email", db.TaiKhoans.Find(hoaDon.MaTK).Email);
        //    //vnpay.AddRequestData("vnp_Inv_Customer", txt_inv_customer.Text.Trim());
        //    //vnpay.AddRequestData("vnp_Inv_Address", txt_inv_addr1.Text.Trim());
        //    //vnpay.AddRequestData("vnp_Inv_Company", txt_inv_company.Text);
        //    //vnpay.AddRequestData("vnp_Inv_Taxcode", txt_inv_taxcode.Text);
        //    //vnpay.AddRequestData("vnp_Inv_Type", cbo_inv_type.SelectedItem.Value);

        //    string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
        //    log.InfoFormat("VNPAY URL: {0}", paymentUrl);
        //    Response.Redirect(paymentUrl);
        //}
        
    }
}
