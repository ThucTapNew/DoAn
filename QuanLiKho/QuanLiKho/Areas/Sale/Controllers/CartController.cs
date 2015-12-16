using QuanLiKho.Areas.Admin.Models;
using QuanLiKho.Areas.Common;
using QuanLiKho.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QuanLiKho.Areas.Sale.Controllers
{
    public class CartController : Controller
    {
        Entities db = new Entities();

        // GET: Sale/Cart
        public ActionResult Index()
        {
            var cart = Session[Common.Common.CartSession];
            var list = new List<HangHoaList>();
            if (cart != null)
            {
                list = (List<HangHoaList>)cart;
            }
            return View(list);
        }
        public ActionResult AddCart(string id, float soLuong, float tienTra, float donGia)
        {
            var kho = new KhoDb();
            var product = kho.ViewDetail(id);
            var session = Session[Common.Common.CartSession];
            if (session != null)
            {
                var list = (List<HangHoaList>)session;
                if (list.Exists(x => x.Product.MaHH == id))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.MaHH == id) item.SoLuong += soLuong;
                    }
                }
                else
                {
                    var item = new HangHoaList();
                    item.Product = product;
                    item.SoLuong = soLuong;
                    list.Add(item);
                }
                return View(list);
            }
            else
            {
                //add hang vao list
                var item = new HangHoaList();
                item.Product = product;
                item.SoLuong = soLuong;
                var list = new List<HangHoaList>();
                list.Add(item);
                Session[Common.Common.CartSession] = list;
                // add hang vao DB
                // tao Ma Hoa Don
                return View(list);
            }

        }
        //delete all gio hang
        public JsonResult DeleteAll()
        {
            Session[Common.Common.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(string id)
        {
            var sessionCart = (List<HangHoaList>)Session[Common.Common.CartSession];
            sessionCart.RemoveAll(x => x.Product.MaHH == id);
            Session[Common.Common.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult UpdateCart(string cartModel)
        {
            var jsoncart = new JavaScriptSerializer().Deserialize<List<HangHoaList>>(cartModel);
            var sessionCart = (List<HangHoaList>)Session[Common.Common.CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsoncart.SingleOrDefault(x => x.Product.MaHH == item.Product.MaHH);
                if (jsonItem != null)
                {
                    item.SoLuong = jsonItem.SoLuong;
                }
            }
            Session[Common.Common.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult Payment()
        {
            var cart = (List<HangHoaList>)Session[Common.Common.CartSession];
            //tinh tong gia tri cua hoa don
            decimal s = 0;
            foreach (var item in cart)
            {
                s = s + Convert.ToDecimal(item.Product.GiaBan * item.SoLuong);
            }
            // Them HoaDon
            // string NV_ID = Main.Controllers.AccountController.ID_NV;

            HoaDon hd = new HoaDon
            {
                MaHD = new GetMa().MaHoaDon(),
                LoaiHD = false,
                NgayTao = DateTime.Now,
                TenTK_NV = Admin.Controllers.AccountController.id_NV,
                TongTien = Convert.ToDouble(s),
                MaDT=null,
            };
            db.HoaDon.Add(hd);
            db.SaveChanges();
            var detail = new KhoDb();
            foreach (var item in cart)
            {
                var hoadon = new CT_HoaDon();
                hoadon.MaHD = hd.MaHD;
                hoadon.SoLuong = item.SoLuong;
                hoadon.MaHH = item.Product.MaHH;
                hoadon.DonGia = item.Product.GiaBan;
                hoadon.LoiNhuan = Convert.ToDecimal(item.Product.GiaBan - item.Product.GiaNhap);
                detail.Add_CT_HoaDon(hoadon);
            }
            return RedirectToAction("Index", "Cart", new { area = "Sale" });
        }
    }
}