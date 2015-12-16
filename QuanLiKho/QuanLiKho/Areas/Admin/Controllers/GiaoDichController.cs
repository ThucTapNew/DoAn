using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLiKho.Models.Entities;
using QuanLiKho.Areas.Admin.Models;

namespace QuanLiKho.Areas.Admin.Controllers
{
    public class GiaoDichController : Controller
    {
        private Entities db = new Entities();
        private GetMa ma = new GetMa();
        // GET: Admin/GiaoDich
        public ActionResult Index()
        {
            var hoaDon = db.HoaDon.Include(h => h.DoiTac).Include(h => h.NhanVien);
            return View(hoaDon.ToList());
        }
        public ActionResult TaoHoaDon()
        {
            var list = new List<CartItem>();
            list = (List<CartItem>)Session["Cart"];
            return PartialView("_CreatePartial", list);
        }

        [HttpPost]
        public ActionResult Create(string maDT, string tongtien)
        {
            var list = (List<CartItem>)Session["Cart"];
            if (list != null)
            {

                HoaDon hd = new HoaDon()
                {
                    MaHD = ma.MaHoaDon(),
                    MaDT = maDT,
                    NgayTao = DateTime.Now,
                    LoaiHD = true,
                    TenTK_NV = "NV001",
                    TongTien = Convert.ToDouble(tongtien)
                };
                db.HoaDon.Add(hd);
                db.SaveChanges();
                Create_CongNo(hd.MaHD);
                foreach (var item in list)
                {
                    CT_HoaDon ct = new CT_HoaDon()
                    {
                        MaHD = hd.MaHD,
                        MaHH = item.Product.MaHH,
                        SoLuong = item.SoLuong,
                        DonGia = item.DonGia
                    };
                    db.CT_HoaDon.Add(ct);
                    HangHoa hh = db.HangHoa.Single(x => x.MaHH == item.Product.MaHH);
                    hh.SoLuong += item.SoLuong;
                    hh.GiaNhap = item.DonGia;
                    db.SaveChanges();
                }

                return Json(Url.Action("Index", "GiaoDich"));
            }
            else
                return Json(Url.Action("Create", "GiaoDich"));
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ct = from p in db.CT_HoaDon
                     where p.MaHD == id
                     select p;
            HoaDon hoadon = db.HoaDon.Find(id);
            if (hoadon ==null)
            {
                return HttpNotFound();
            }
            ViewBag.HD = hoadon.MaHD;
            ViewBag.Time = hoadon.NgayTao;
            ViewBag.Tongtien = hoadon.TongTien;
            return View(ct.ToList());
        }
        //Tạo gợi ý cho text Mặt hàng 
        public JsonResult ProductAutoComplete(string text)
        {
            var results = new List<KeyValuePair<string, string>>();
            var ds = db.HangHoa.Where(x => x.TenHH.ToUpper().Contains(text.ToUpper()))
                 .Select(x => new { x.TenHH, x.MaHH }).ToList();
            foreach (var item in ds)
            {
                results.Add(new KeyValuePair<string, string>(item.MaHH, item.TenHH));
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        // Tạo gợi ý cho text đối tác
        public JsonResult DoiTacAutoComplete(string text)
        {
            var results = new List<KeyValuePair<string, string>>();
            var ds = db.DoiTac.Where(x => x.FullName.ToUpper().Contains(text.ToUpper()))
                 .Select(x => new { x.MaDT, x.FullName }).ToList();
            foreach (var item in ds)
            {
                results.Add(new KeyValuePair<string, string>(item.MaDT, item.FullName));
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTongTien()
        {
            var list = (List<CartItem>)Session["Cart"];
            double result = 0;
            foreach (var item in list)
            {
                result += item.SoLuong * item.DonGia;
            }
            return Json(result.ToString(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddToCart(string id, string soluong, string dongia)
        {
            // lấy mặt hàng
            var cart = Session["Cart"];
            var list = new List<CartItem>();
            if (id != null)
            {
                if (cart != null)
                {
                    list = (List<CartItem>)cart;
                    bool alreadyItem = list.Any(x => x.Product.MaHH == id);
                    if (alreadyItem)
                    {
                        foreach (var item in list)
                        {
                            if (item.Product.MaHH == id)
                            {
                                item.SoLuong += Convert.ToSingle(soluong);
                                item.DonGia = Convert.ToSingle(dongia);
                            }
                        }

                    }
                    else
                    {
                        var item = new CartItem()
                        {
                            Product = db.HangHoa.Where(x => x.MaHH == id).SingleOrDefault(),
                            SoLuong = Convert.ToSingle(soluong),
                            DonGia = Convert.ToSingle(dongia)
                        };
                        list.Add(item);
                    }
                    Session["Cart"] = list;
                }
                else
                {
                    var item = new CartItem()
                    {
                        Product = db.HangHoa.Where(x => x.MaHH == id).SingleOrDefault(),
                        SoLuong = Convert.ToSingle(soluong),
                        DonGia = Convert.ToSingle(dongia)
                    };
                    list.Add(item);
                    Session["Cart"] = list;
                }
            }
            return PartialView("_ListItem", list);
        }
 
        public void Create_CongNo(string maHD)
        {
            CongNo cn = new CongNo()
            {
                MaCN = ma.MaCongNo(),
                MaHD = maHD
            };
            db.CongNo.Add(cn);
            db.SaveChanges();
        }
        // Lọc theo loại hóa đơn
        public ActionResult LoaiHoaDon(string gender)
        {
            var ds = from p in db.HoaDon select p;
            switch (gender)
            {
                case "nhap":
                    {
                        ds = from p in db.HoaDon
                             where p.LoaiHD == true
                             select p;
                        break;
                    }
                case "xuat":
                    {
                        ds = from p in db.HoaDon
                             where p.LoaiHD == false
                             select p;
                        break;
                    }
            }
            return PartialView("_GiaoDichPartial", ds.ToList());
        }
          
        // Tìm kiếm  
        public ActionResult Search(string  gender)
        {
            if (gender != null && gender != "")
            {
                var ds = from p in db.HoaDon
                         where p.MaHD.ToUpper().Contains(gender.ToUpper()) 
                         || p.DoiTac.FullName.ToUpper().Contains(gender.ToUpper())
                         || p.NhanVien.FullName.ToUpper().Contains(gender.ToUpper())
                         select p;
                return PartialView("_GiaoDichPartial", ds.ToList());
            }
            else
            {
                return PartialView("_GiaoDichPartial");
            }
        }
        //Lọc theo thời gian
        public ActionResult LocThoiGian(string gender)
        {
            var ds = from p in db.HoaDon select p;
            switch(gender)
            {
                case "today":
                    {
                        ds= from p in db.HoaDon
                            where p.NgayTao.Day==DateTime.Now.Day
                            select p;
                        break;
                    }
                case "month":
                    {
                        ds = from p in db.HoaDon
                             where p.NgayTao.Month == DateTime.Now.Month
                             select p;
                        break;
                    }
            }
            return PartialView("_GiaoDichPartial", ds.ToList());

        }
        // GET: Admin/GiaoDich/Details/5

   

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
