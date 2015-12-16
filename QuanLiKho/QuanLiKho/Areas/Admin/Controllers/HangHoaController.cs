using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLiKho.Models.Entities;
using QuanLiKho.Areas.Common;
using QuanLiKho.Areas.Admin.Models;

namespace QuanLiKho.Areas.Admin.Controllers
{
    public class HangHoaController : BaseController
    {
        private Entities db = new Entities();

        // GET: Admin/HangHoa
        public ActionResult Index()
        {
            var dsLoaiHang = from p in db.LoaiHang
                             select p;
            var dsNhomHang = from p in db.NhomHang
                             select p;
            ViewData["dsNhomHang"] = dsNhomHang.ToList();
            return View(dsLoaiHang.ToList());
        }
        public ActionResult Search(string gender)
        {
            if (gender != null && gender != "")
            {
                var ds = from p in db.HangHoa
                         where p.TenHH.ToUpper().Contains(gender.ToUpper()) || p.MaHH.ToUpper().Contains(gender.ToUpper())
                         select p;
                return PartialView("_IndexHangHoa", ds.ToList());
            }
            else
            {
                return PartialView("_IndexHangHoa");
            }
        }
        public ActionResult LocLoaiHang(string gender)
        {
            if (gender != null && gender != "")
            {
                var ds = from p in db.HangHoa
                         where p.MaLH == gender
                         select p;
                return PartialView("_IndexHangHoa", ds.ToList());
            }
            else
            {
                var ds = from p in db.HangHoa
                         select p;
                return PartialView("_IndexHangHoa", ds.ToList());
            }
        }
        public ActionResult LocNhomHang(string gender)
        {
            if (gender != null && gender != "")
            {
                var ds = from p in db.HangHoa
                         where p.MaNH == gender
                         select p;
                return PartialView("_IndexHangHoa", ds.ToList());
            }
            else
            {
                var ds = from p in db.HangHoa
                         select p;
                return PartialView("_IndexHangHoa", ds.ToList());
            }
        }
        public ActionResult TonKho(string gender)
        {
            var ds = from p in db.HangHoa select p;
            switch (gender)
            {
                case "duoidinhmuc":
                    {
                        ds = from p in db.HangHoa
                             where p.SoLuong <= p.DinhMuc
                             select p;
                        break;
                    }
                case "vuotdinhmuc":
                    {
                        ds = from p in db.HangHoa
                             where p.SoLuong > p.DinhMuc
                             select p;
                        break;
                    }
                case "dangkinhdoanh":
                    {
                        ds = from p in db.HangHoa
                             where p.TinhTrang == true
                             select p;
                        break;
                    }
                case "ngungkinhdoanh":
                    {
                        ds = from p in db.HangHoa
                             where p.TinhTrang == false
                             select p;
                        break;
                    }

            }
            return PartialView("_IndexHangHoa", ds.ToList());
        }

        // GET: Admin/HangHoa/Details/5
        [HasCredential(RoleID = "VIEW_HH")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoa.Find(id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // GET: Admin/HangHoa/Create
        public ActionResult Create()
        {
            ViewBag.MaLH = new SelectList(db.LoaiHang, "MaLH", "TenLH");
            ViewBag.MaNH = new SelectList(db.NhomHang, "MaNH", "TenNH");
            return View();
        }

        // POST: Admin/HangHoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHH,TenHH,SoLuong,KichThuoc,KhoiLuong,DVT,NgaySX,HanSD,LoSX,GiaBan,GiaNhap,TrinhTrang,DinhMuc,MaNH,MaLH")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                hangHoa.MaHH = new GetMa().MaHangHoa();
                db.HangHoa.Add(hangHoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLH = new SelectList(db.LoaiHang, "MaLH", "TenLH", hangHoa.MaLH);
            ViewBag.MaNH = new SelectList(db.NhomHang, "MaNH", "TenNH", hangHoa.MaNH);
            return View(hangHoa);
        }

        // GET: Admin/HangHoa/Edit/5
        [HasCredential(RoleID = "EDIT_HH")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoa.Find(id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLH = new SelectList(db.LoaiHang, "MaLH", "TenLH", hangHoa.MaLH);
            ViewBag.MaNH = new SelectList(db.NhomHang, "MaNH", "TenNH", hangHoa.MaNH);
            return View(hangHoa);
        }

        // POST: Admin/HangHoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "EDIT_HH")]
        public ActionResult Edit([Bind(Include = "MaHH,TenHH,SoLuong,KichThuoc,KhoiLuong,NgaySX,HanSD,LoSX,GiaBan,GiaNhap,TrinhTrang,DinhMuc,MaNH,MaLH")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangHoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLH = new SelectList(db.LoaiHang, "MaLH", "TenLH", hangHoa.MaLH);
            ViewBag.MaNH = new SelectList(db.NhomHang, "MaNH", "TenNH", hangHoa.MaNH);
            return View(hangHoa);
        }

        // GET: Admin/HangHoa/Delete/5
        [HasCredential(RoleID = "DEL_HH")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoa.Find(id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // POST: Admin/HangHoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "DEL_HH")]
        public ActionResult DeleteConfirmed(string id)
        {
            HangHoa hangHoa = db.HangHoa.Find(id);
            db.HangHoa.Remove(hangHoa);
            db.SaveChanges();
            return RedirectToAction("Index");
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
