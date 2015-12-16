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
    public class NganQuyController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/NganQuy
        public ActionResult Index()
        {
            var nganQuy = db.NganQuy.Include(n => n.DoiTac);
            return View(nganQuy.ToList());
        }

        // GET: Admin/NganQuy/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganQuy nganQuy = db.NganQuy.Find(id);
            if (nganQuy == null)
            {
                return HttpNotFound();
            }
            return View(nganQuy);
        }

        // GET: Admin/NganQuy/Create
        public ActionResult Create()
        {
            ViewBag.MaDT = new SelectList(db.DoiTac, "MaDT", "MaSoThue");
            return View();
        }

        // POST: Admin/NganQuy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNQ,MaDT,ThoiGian,SoTien")] NganQuy nganQuy)
        {
            if (ModelState.IsValid)
            {
                db.NganQuy.Add(nganQuy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDT = new SelectList(db.DoiTac, "MaDT", "MaSoThue", nganQuy.MaDT);
            return View(nganQuy);
        }

        // GET: Admin/NganQuy/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganQuy nganQuy = db.NganQuy.Find(id);
            if (nganQuy == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDT = new SelectList(db.DoiTac, "MaDT", "MaSoThue", nganQuy.MaDT);
            return View(nganQuy);
        }

        // POST: Admin/NganQuy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNQ,MaDT,ThoiGian,SoTien")] NganQuy nganQuy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nganQuy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDT = new SelectList(db.DoiTac, "MaDT", "MaSoThue", nganQuy.MaDT);
            return View(nganQuy);
        }

        // GET: Admin/NganQuy/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NganQuy nganQuy = db.NganQuy.Find(id);
            if (nganQuy == null)
            {
                return HttpNotFound();
            }
            return View(nganQuy);
        }

        // POST: Admin/NganQuy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NganQuy nganQuy = db.NganQuy.Find(id);
            db.NganQuy.Remove(nganQuy);
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
