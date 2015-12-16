
using QuanLiKho.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using System.Net;

namespace QuanLiKho.Areas.Inventory.Controllers
{

    public class GiaoDichController : Controller
    {
        private Entities db = new Entities();
        // GET: Inventory/GiaoDich
        public ActionResult Index()
        {
            return View();
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
            if (hoadon == null)
            {
                return HttpNotFound();
            }
            ViewBag.HD = hoadon.MaHD;
            ViewBag.Time = hoadon.NgayTao;
            ViewBag.Tongtien = hoadon.TongTien;
            return View(ct.ToList());
        }
        // POST: Admin/GiaoDich/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    }

}