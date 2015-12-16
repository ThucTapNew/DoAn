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
    public class CuaHangController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/CuaHang
        public ActionResult Index()
        {
            var cuaHang = db.CuaHang.Include(c => c.KhoHang);
            return View(cuaHang.ToList());
        }

        // GET: Admin/CuaHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = db.CuaHang.Find(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            return View(cuaHang);
        }

        // GET: Admin/CuaHang/Create
        public ActionResult Create()
        {
            ViewBag.MaKho = new SelectList(db.KhoHang, "MaKho", "TenKho");
            return View();
        }

        // POST: Admin/CuaHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCH,TenCH,MaKho")] CuaHang cuaHang)
        {
            if (ModelState.IsValid)
            {
                db.CuaHang.Add(cuaHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKho = new SelectList(db.KhoHang, "MaKho", "TenKho", cuaHang.MaKho);
            return View(cuaHang);
        }

        // GET: Admin/CuaHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = db.CuaHang.Find(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKho = new SelectList(db.KhoHang, "MaKho", "TenKho", cuaHang.MaKho);
            return View(cuaHang);
        }

        // POST: Admin/CuaHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCH,TenCH,MaKho")] CuaHang cuaHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuaHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKho = new SelectList(db.KhoHang, "MaKho", "TenKho", cuaHang.MaKho);
            return View(cuaHang);
        }

        // GET: Admin/CuaHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuaHang cuaHang = db.CuaHang.Find(id);
            if (cuaHang == null)
            {
                return HttpNotFound();
            }
            return View(cuaHang);
        }

        // POST: Admin/CuaHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CuaHang cuaHang = db.CuaHang.Find(id);
            db.CuaHang.Remove(cuaHang);
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
