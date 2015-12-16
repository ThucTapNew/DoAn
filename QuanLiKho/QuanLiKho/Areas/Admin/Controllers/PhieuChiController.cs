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
    public class PhieuChiController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/PhieuChi
        public ActionResult Index()
        {
            var phieuThuChi = db.PhieuThuChi.Include(p => p.HoaDon);
            return View(phieuThuChi.ToList());
        }
        // GET: Admin/PhieuChi/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.CongNo, "MaHD", "MaHD");
            return View();
        }

        // POST: Admin/PhieuChi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPH,MaDT,MaHD,Thoigian,GiaTri")] PhieuThuChi phieuThuChi)
        {
            if (ModelState.IsValid)
            {
                phieuThuChi.MaPH = new GetMa().MaPhieu();
                phieuThuChi.ThoiGian = DateTime.Now;
                db.PhieuThuChi.Add(phieuThuChi);
                CongNo cn = db.CongNo.Single(x => x.MaHD == phieuThuChi.MaHD);
                db.CongNo.Remove(cn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.CongNo, "MaHD", "MaHD", phieuThuChi.MaHD);
            return View(phieuThuChi);
        }
        //Load Thông tin hóa đơn khi select từ dropdownlist
        public JsonResult GetInfor(string maHD)
        {
            HoaDon hd = db.HoaDon.Single(x => x.MaHD == maHD);
            var result = new
            {
                time = hd.NgayTao.ToString(),
                doitac = hd.DoiTac.FullName,
                tongtien = hd.TongTien,
                nv = hd.NhanVien.FullName
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/PhieuChi/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThuChi phieuThuChi = db.PhieuThuChi.Find(id);
            if (phieuThuChi == null)
            {
                return HttpNotFound();
            }
            return View(phieuThuChi);
        }

        // POST: Admin/PhieuChi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuThuChi phieuThuChi = db.PhieuThuChi.Find(id);
            db.PhieuThuChi.Remove(phieuThuChi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult LocThoiGian(string gender)
        {
            var ds = from p in db.PhieuThuChi select p;
            switch (gender)
            {
                case "day":
                    {
                        ds = from p in db.PhieuThuChi
                             where p.ThoiGian.Day == DateTime.Now.Day
                             select p;
                        break;
                    }
                case "month":
                    {
                        ds = from p in db.PhieuThuChi
                             where p.ThoiGian.Month == DateTime.Now.Month
                             select p;
                        break;
                    }
            }
            return PartialView("_PhieuChiPartial", ds.ToList());
        }
        public ActionResult Search(string gender)
        {
            if (!String.IsNullOrEmpty(gender))
            {
                var ds = db.PhieuThuChi.Where
                    (
                    x => x.MaPH.ToUpper().Contains(gender.ToUpper())
                    || x.MaHD.ToUpper().Contains(gender.ToUpper())
                    || x.HoaDon.DoiTac.FullName.ToUpper().Contains(gender.ToUpper())
                    );
                return PartialView("_PhieuChiPartial", ds.ToList());
            }
            else return PartialView("_PhieuChiPartial");
        }
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
