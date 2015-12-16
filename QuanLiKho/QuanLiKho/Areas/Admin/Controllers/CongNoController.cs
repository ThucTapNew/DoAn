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
    public class CongNoController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/CongNo
        public ActionResult Index()
        {
            var congNo = from p in db.CongNo
                         group p by p.HoaDon.DoiTac into g
                         select new TongNo()
                         {
                             DoiTac = g.Key,
                             GiaTri = g.Sum(x => x.HoaDon.TongTien)
                         };

            return View(congNo.ToList());
        }

        // GET: Admin/CongNo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ds = db.CongNo.Where(x => x.HoaDon.MaDT == id);
            ViewBag.DT = db.DoiTac.Single(x => x.MaDT == id).FullName;
            return View(ds.ToList());
        }

        // GET: Admin/CongNo/Create
  
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
