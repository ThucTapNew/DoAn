using QuanLiKho.Models.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Net;

namespace QuanLiKho.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        // GET: Admin/Report
        Entities db = new Entities();
        public ActionResult Index()
        {
            return View();
        }
        public string SaleReport(string care, string time, string type)
        {
            if (type == "chart")
            {
                if ((care == null && time == null) || (time == null && care == "time"))
                {
                    var list = from q in db.BC_CuoiNgay
                               group q by q.NgayTao.Month
                    into kq
                               select new { NgayTao = kq.Key, TongThu = kq.Sum(t => t.TongThu) };
                    var chart = new Chart(width: 300, height: 200)
          .AddSeries("Default",
                                   xValue: list, xField: "NgayTao",
                                   yValues: list, yFields: "TongThu")
                                    .GetBytes("png");

                    return Convert.ToBase64String(chart);
                }
                else if (care == "time" && time == "month")
                {
                    var list = from q in db.BC_CuoiNgay
                               group q by q.NgayTao.Month
                     into kq
                               select new { NgayTao = kq.Key, TongThu = kq.Sum(t => t.TongThu) };
                    var chart = new Chart(width: 300, height: 200)
          .AddSeries("Default",
                                   xValue: list, xField: "NgayTao",
                                   yValues: list, yFields: "TongThu")
                                    .GetBytes("png");

                    return Convert.ToBase64String(chart);
                }
                else if ((care == "profit" && time == "month") || (care == "profit" && time == null))
                {
                    var list = from p in db.CT_HoaDon
                               join q in db.HoaDon
                               on p.MaHD equals q.MaHD
                               group p by q.NgayTao.Month
                    into kq
                               where kq.Key == DateTime.Now.Month
                               select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                    var chart = new Chart(width: 300, height: 200)
                        .AddTitle("Báo cáo lợi nhuận")
                        .AddSeries("Default",
                            xValue: list, xField: "NgayTao",
                            yValues: list, yFields: "LoiNhuan")
                            .GetBytes("png");
                    //return File(chart, "image/jpeg");
                    return Convert.ToBase64String(chart);
                }
                else if ((care == "staff" && time == "month")|| (care == "staff" && time == null))
                {
                    var list = from p in db.HoaDon
                               join q in db.NhanVien
                               on p.TenTK_NV equals q.TenTK
                               group p by new
                               {
                                   q.FullName,
                                   p.NgayTao.Month
                               }
                     into kq
                               where kq.Key.Month == DateTime.Now.Month
                               select new { TenNV = kq.Key, DoanhThu = kq.Sum(c => c.TongTien) };
                    var chart = new Chart(width: 300, height: 200)
                        .AddTitle("Báo cáo theo nhân viên")
                        .AddSeries("default",
                                    xValue: list, xField: "TenNV",
                                    yValues: list, yFields: "DoanhThu")
                                    .GetBytes("png");
                    //return File(chart, "image/jpeg");
                    return Convert.ToBase64String(chart);
                }
                else if (care == "time" && time == "season")
                {
                    int timer = DateTime.Now.Month;
                    if (timer == 1 || timer == 2 || timer == 3)
                    {
                        var list = from p in db.CT_HoaDon
                                   join q in db.HoaDon
                                   on p.MaHD equals q.MaHD
                                   group p by q.NgayTao.Month
                              into kq
                                   where kq.Key == 1 || kq.Key == 2 || kq.Key == 3
                                   select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Báo cáo lợi nhuận")
                            .AddSeries("Default",
                                xValue: list, xField: "NgayTao",
                                yValues: list, yFields: "LoiNhuan")
                                .GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else if (timer == 4 || timer == 5 || timer == 6)
                    {
                        var list = from p in db.CT_HoaDon
                                   join q in db.HoaDon
                                   on p.MaHD equals q.MaHD
                                   group p by q.NgayTao.Month
                             into kq
                                   where kq.Key == 4 || kq.Key == 5 || kq.Key == 6
                                   select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Báo cáo lợi nhuận")
                            .AddSeries("Default",
                                xValue: list, xField: "NgayTao",
                                yValues: list, yFields: "LoiNhuan")
                                .GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else if (timer == 7 || timer == 8 || timer == 9)
                    {
                        var list = from p in db.CT_HoaDon
                                   join q in db.HoaDon
                                   on p.MaHD equals q.MaHD
                                   group p by q.NgayTao.Month
                             into kq
                                   where kq.Key == 7 || kq.Key == 8 || kq.Key == 9
                                   select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Báo cáo lợi nhuận")
                            .AddSeries("Default",
                                xValue: list, xField: "NgayTao",
                                yValues: list, yFields: "LoiNhuan")
                                .GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else
                    {
                        var list = from p in db.CT_HoaDon
                                   join q in db.HoaDon
                                   on p.MaHD equals q.MaHD
                                   group p by q.NgayTao.Month
                             into kq
                                   where kq.Key == 10 || kq.Key == 11 || kq.Key == 12
                                   select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Báo cáo lợi nhuận")
                            .AddSeries("Default",
                                xValue: list, xField: "NgayTao",
                                yValues: list, yFields: "LoiNhuan")
                                .GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                }
                else if (care == "time" && time == "year")
                {
                    var list = from p in db.CT_HoaDon
                               join q in db.HoaDon
                               on p.MaHD equals q.MaHD
                               group p by q.NgayTao.Year
                  into kq
                               where kq.Key == DateTime.Now.Year
                               select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                    var chart = new Chart(width: 300, height: 200)
                        .AddTitle("Báo cáo lợi nhuận năm nay")
                        .AddSeries("Default",
                            xValue: list, xField: "NgayTao",
                            yValues: list, yFields: "LoiNhuan")
                            .GetBytes("png");
                    return Convert.ToBase64String(chart);
                }
                else if (time == "year" && care == "staff")
                {
                    var list = from p in db.HoaDon
                               join q in db.NhanVien
                               on p.TenTK_NV equals q.TenTK
                               group p by new
                               {
                                   q.FullName,
                                   p.NgayTao.Year
                               }
                     into kq
                               where kq.Key.Year == DateTime.Now.Year
                               select new { TenNV = kq.Key, DoanhThu = kq.Sum(c => c.TongTien) };
                    var chart = new Chart(width: 300, height: 200)
                        .AddTitle("Báo cáo theo nhân viên")
                        .AddSeries("default",
                                    xValue: list, xField: "TenNV",
                                    yValues: list, yFields: "DoanhThu")
                                    .GetBytes("png");
                    return Convert.ToBase64String(chart);
                }
                else if (care == "staff" && time == "season")
                {
                    int timer = DateTime.Now.Month;
                    if (timer == 1 || timer == 2 || timer == 3)
                    {
                        var list = from p in db.HoaDon
                                   join q in db.NhanVien
                                   on p.TenTK_NV equals q.TenTK
                                   group p by new
                                   {
                                       p.NgayTao.Month,
                                       q.FullName
                                   }
                                   into kq
                                   where (kq.Key.Month == 1) || (kq.Key.Month == 2) || (kq.Key.Month == 3)
                                   select new
                                   {
                                       TenNv = kq.Key.FullName,
                                       DoanhThu = kq.Sum(c => c.TongTien)
                                   };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Lọc theo doanh thu của nhân viên theo quý")
                            .AddSeries("Ddfault",
                            xValue: list, xField: "TenNv",
                            yValues: list, yFields: "DoanhThu").GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else if (timer == 4 || timer == 5 || timer == 6)
                    {
                        var list = from p in db.HoaDon
                                   join q in db.NhanVien
                                   on p.TenTK_NV equals q.TenTK
                                   group p by new
                                   {
                                       p.NgayTao.Month,
                                       q.FullName
                                   }
                                                  into kq
                                   where (kq.Key.Month == 4) || (kq.Key.Month == 5) || (kq.Key.Month == 6)
                                   select new
                                   {
                                       TenNv = kq.Key.FullName,
                                       DoanhThu = kq.Sum(c => c.TongTien)
                                   };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Lọc theo doanh thu của nhân viên theo quý")
                            .AddSeries("Ddfault",
                            xValue: list, xField: "TenNv",
                            yValues: list, yFields: "DoanhThu").GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else if (timer == 7 || timer == 8 || timer == 9)
                    {
                        var list = from p in db.HoaDon
                                   join q in db.NhanVien
                                   on p.TenTK_NV equals q.TenTK
                                   group p by new
                                   {
                                       p.NgayTao.Month,
                                       q.FullName
                                   }
                                  into kq
                                   where (kq.Key.Month == 7) || (kq.Key.Month == 8) || (kq.Key.Month == 8)
                                   select new
                                   {
                                       TenNv = kq.Key.FullName,
                                       DoanhThu = kq.Sum(c => c.TongTien)
                                   };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Lọc theo doanh thu của nhân viên theo quý")
                            .AddSeries("Ddfault",
                            xValue: list, xField: "TenNv",
                            yValues: list, yFields: "DoanhThu").GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else
                    {
                        var list = from p in db.HoaDon
                                   join q in db.NhanVien
                                   on p.TenTK_NV equals q.TenTK
                                   group p by new
                                   {
                                       p.NgayTao.Month,
                                       q.FullName
                                   }
                                  into kq
                                   where (kq.Key.Month == 10) || (kq.Key.Month == 11) || (kq.Key.Month == 12)
                                   select new
                                   {
                                       TenNv = kq.Key.FullName,
                                       DoanhThu = kq.Sum(c => c.TongTien)
                                   };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Lọc theo doanh thu của nhân viên theo quý")
                            .AddSeries("Ddfault",
                            xValue: list, xField: "TenNv",
                            yValues: list, yFields: "DoanhThu").GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }

                }
                else if (care == "staff" && time == "year")
                {
                    var list = from p in db.HoaDon
                               join q in db.NhanVien
                               on p.TenTK_NV equals q.TenTK
                               group p by new
                               {
                                   q.FullName,
                                   p.NgayTao.Year
                               }
                    into kq
                               where kq.Key.Year == DateTime.Now.Year
                               select new { TenNV = kq.Key, DoanhThu = kq.Sum(c => c.TongTien) };
                    var chart = new Chart(width: 300, height: 200)
                        .AddTitle("Báo cáo theo nhân viên")
                        .AddSeries("default",
                                    xValue: list, xField: "TenNV",
                                    yValues: list, yFields: "DoanhThu")
                                    .GetBytes("png");
                    return Convert.ToBase64String(chart);
                }
                else if (time == "year" && care == "profit")
                {
                    var list = from p in db.CT_HoaDon
                               join q in db.HoaDon
                               on p.MaHD equals q.MaHD
                               group p by q.NgayTao.Year
                    into kq
                               where kq.Key == DateTime.Now.Month
                               select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                    var chart = new Chart(width: 300, height: 200)
                        .AddTitle("Báo cáo theo lợi nhuận theo năm")
                        .AddSeries("default",
                                    xValue: list, xField: "TenNV",
                                    yValues: list, yFields: "DoanhThu")
                                    .GetBytes("png");
                    return Convert.ToBase64String(chart);
                }
                else if (time == "season" && care == "profit")
                {
                    int timer = DateTime.Now.Month;
                    if (timer == 1 || timer == 2 || timer == 3)
                    {
                        var list = from p in db.CT_HoaDon
                                   join q in db.HoaDon
                                   on p.MaHD equals q.MaHD
                                   group p by q.NgayTao.Month
                    into kq
                                   where (kq.Key == 1) || (kq.Key == 2) || (kq.Key == 3)
                                   select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Báo cáo theo lợi nhuận theo năm")
                            .AddSeries("default",
                                        xValue: list, xField: "TenNV",
                                        yValues: list, yFields: "DoanhThu")
                                        .GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else if (timer == 4 || timer == 5 || timer == 6)
                    {
                        var list = from p in db.CT_HoaDon
                                   join q in db.HoaDon
                                   on p.MaHD equals q.MaHD
                                   group p by q.NgayTao.Month
                    into kq
                                   where (kq.Key == 4) || (kq.Key == 5) || (kq.Key == 6)
                                   select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Báo cáo theo lợi nhuận theo năm")
                            .AddSeries("default",
                                        xValue: list, xField: "TenNV",
                                        yValues: list, yFields: "DoanhThu")
                                        .GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else if (timer == 7 || timer == 8 || timer == 9)
                    {
                        var list = from p in db.CT_HoaDon
                                   join q in db.HoaDon
                                   on p.MaHD equals q.MaHD
                                   group p by q.NgayTao.Month
                    into kq
                                   where (kq.Key == 7) || (kq.Key == 8) || (kq.Key == 9)
                                   select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Báo cáo theo lợi nhuận theo năm")
                            .AddSeries("default",
                                        xValue: list, xField: "TenNV",
                                        yValues: list, yFields: "DoanhThu")
                                        .GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                    else
                    {
                        var list = from p in db.CT_HoaDon
                                   join q in db.HoaDon
                                   on p.MaHD equals q.MaHD
                                   group p by q.NgayTao.Month
                    into kq
                                   where (kq.Key == 10) || (kq.Key == 11) || (kq.Key == 12)
                                   select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
                        var chart = new Chart(width: 300, height: 200)
                            .AddTitle("Báo cáo theo lợi nhuận theo năm")
                            .AddSeries("default",
                                        xValue: list, xField: "TenNV",
                                        yValues: list, yFields: "DoanhThu")
                                        .GetBytes("png");
                        return Convert.ToBase64String(chart);
                    }
                }

            }
            return string.Empty;
        }

        public string Search(DateTime time1, DateTime time2)
        {
            var list = from p in db.CT_HoaDon
                       join q in db.HoaDon
                       on p.MaHD equals q.MaHD
                       group p by q.NgayTao
                   into kq
                       where kq.Key >= time1 && kq.Key <= time2
                       select new { NgayTao = kq.Key, LoiNhuan = kq.Sum(c => c.LoiNhuan) };
            var chart = new Chart(width: 700, height: 900)
                .AddTitle("Báo cáo lợi nhuận")
                .AddSeries("Default",
                    xValue: list, xField: "NgayTao",
                    yValues: list, yFields: "LoiNhuan")
                    .GetBytes("png");
            //return File(chart, "image/jpeg");
            return Convert.ToBase64String(chart);
        }








        //Report doanh thu cua nhan vien theo quy hien tai


        //report doanh thu cua nhan vien theo nam hien tai

        /*  public ActionResult CreatePie()
          {
              //Create bar chart
              var chart = new Chart(width: 300, height: 200)
              .AddSeries(chartType: "pie",
                              xValue: new[] { "10 ", "50", "30 ", "70" },
                              yValues: new[] { "50", "70", "90", "110" })
                              .GetBytes("png");
              return File(chart, "image/bytes");
          }

          public ActionResult CreateLine()
          {
              //Create bar chart
              var chart = new Chart(width: 600, height: 200)
              .AddSeries(chartType: "line",
                              xValue: new[] { "10 ", "50", "30 ", "70" },
                              yValues: new[] { "50", "70", "90", "110" })
                              .GetBytes("png");
              return File(chart, "image/bytes");
          }*/


    }
}