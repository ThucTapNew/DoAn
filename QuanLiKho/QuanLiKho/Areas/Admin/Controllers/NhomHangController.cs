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
    public class NhomHangController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/NhomHang
        public ActionResult Index()
        {
            var dsNhomHang = db.NhomHang.Include(n => n.LoaiHang);
            return View(dsNhomHang.ToList());
        }

        // GET: Admin/NhomHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhomHang nhomHang = db.NhomHang.Find(id);
            if (nhomHang == null)
            {
                return HttpNotFound();
            }
            return View(nhomHang);
        }

        // GET: Admin/NhomHang/Create
        public ActionResult Create()
        {
            ViewBag.MaLH = new SelectList(db.LoaiHang, "MaLH", "TenLH");

            return View();
        }

        // POST: Admin/NhomHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNH,TenNH,MaLH")] NhomHang nhomHang)
        {
            if (ModelState.IsValid)
            {
                string maNH = "";
                if (db.NhomHang.Count() != 0)
                {
                    var Nh = (from p in db.NhomHang
                              orderby p.MaNH descending
                              select p).Skip(0).Take(1);
                    string numberString = Nh.ToList()[0].MaNH.Substring(2);
                    int number = Convert.ToInt32(numberString);
                    number++;
                    numberString = number.ToString();
                    while (numberString.Length < 5)
                    {
                        numberString = 0 + numberString;
                    }
                    maNH = "NH" + numberString;
                }
                else
                {
                    maNH = "NH00001";
                }
                nhomHang.MaNH = maNH;
                db.NhomHang.Add(nhomHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLH = new SelectList(db.LoaiHang, "MaLH", "TenLH", nhomHang.MaLH);
            return View(nhomHang);
        }

        // GET: Admin/NhomHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhomHang nhomHang = db.NhomHang.Find(id);
            if (nhomHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLH = new SelectList(db.LoaiHang, "MaLH", "TenLH", nhomHang.MaLH);
            return View(nhomHang);
        }

        // POST: Admin/NhomHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNH,TenNH,MaLH")] NhomHang nhomHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhomHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLH = new SelectList(db.LoaiHang, "MaLH", "TenLH", nhomHang.MaLH);
            return View(nhomHang);
        }

        // GET: Admin/NhomHang/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhomHang nhomHang = db.NhomHang.Find(id);
            if (nhomHang == null)
            {
                return HttpNotFound();
            }
            return View(nhomHang);
        }

        // POST: Admin/NhomHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhomHang nhomHang = db.NhomHang.Find(id);
            db.NhomHang.Remove(nhomHang);
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
