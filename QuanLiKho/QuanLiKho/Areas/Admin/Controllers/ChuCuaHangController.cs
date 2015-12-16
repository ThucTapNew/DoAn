using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLiKho.Models.Entities;

namespace QuanLiKho.Areas.Admin.Controllers
{
    public class ChuCuaHangController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/ChuCuaHang
        public ActionResult Index()
        {
            var chuCuaHang = db.ChuCuaHang.Include(c => c.CuaHang);
            return View(chuCuaHang.ToList());
        }

        // GET: Admin/ChuCuaHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuCuaHang chuCuaHang = db.ChuCuaHang.Find(id);
            if (chuCuaHang == null)
            {
                return HttpNotFound();
            }
            return View(chuCuaHang);
        }

        // GET: Admin/ChuCuaHang/Create
        public ActionResult Create()
        {
            ViewBag.MaCH = new SelectList(db.CuaHang, "MaCH", "TenCH");
            return View();
        }

        // POST: Admin/ChuCuaHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenTK,FullName,SDT,Email,DiaChi,Pwd,MaCH,MaPQ")] ChuCuaHang chuCuaHang)
        {
            if (ModelState.IsValid)
            {
                db.ChuCuaHang.Add(chuCuaHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCH = new SelectList(db.CuaHang, "MaCH", "TenCH", chuCuaHang.MaCH);
            return View(chuCuaHang);
        }

        // GET: Admin/ChuCuaHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuCuaHang chuCuaHang = db.ChuCuaHang.Find(id);
            if (chuCuaHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCH = new SelectList(db.CuaHang, "MaCH", "TenCH", chuCuaHang.MaCH);
            return View(chuCuaHang);
        }

        // POST: Admin/ChuCuaHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenTK,FullName,SDT,Email,DiaChi,Pwd,MaCH,MaPQ")] ChuCuaHang chuCuaHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chuCuaHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCH = new SelectList(db.CuaHang, "MaCH", "TenCH", chuCuaHang.MaCH);
            return View(chuCuaHang);
        }

        // GET: Admin/ChuCuaHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuCuaHang chuCuaHang = db.ChuCuaHang.Find(id);
            if (chuCuaHang == null)
            {
                return HttpNotFound();
            }
            return View(chuCuaHang);
        }

        // POST: Admin/ChuCuaHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ChuCuaHang chuCuaHang = db.ChuCuaHang.Find(id);
            db.ChuCuaHang.Remove(chuCuaHang);
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
