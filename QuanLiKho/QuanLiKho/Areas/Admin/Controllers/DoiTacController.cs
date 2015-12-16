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
    public class DoiTacController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/DoiTac
        public ActionResult Index()
        {
            return View(db.DoiTac.ToList());
        }

        // GET: Admin/DoiTac/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiTac doiTac = db.DoiTac.Find(id);
            if (doiTac == null)
            {
                return HttpNotFound();
            }
            return View(doiTac);
        }

        // GET: Admin/DoiTac/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DoiTac/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDT,MaSoThue,FullName,SDT,Email,DiaChi")] DoiTac doiTac)
        {
            if (ModelState.IsValid)
            {
                doiTac.MaDT = new GetMa().MaDoiTac();
                db.DoiTac.Add(doiTac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doiTac);
        }

        // GET: Admin/DoiTac/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiTac doiTac = db.DoiTac.Find(id);
            if (doiTac == null)
            {
                return HttpNotFound();
            }
            return View(doiTac);
        }

        // POST: Admin/DoiTac/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDT,MaSoThue,FullName,SDT,Email,DiaChi")] DoiTac doiTac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doiTac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doiTac);
        }

        // GET: Admin/DoiTac/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiTac doiTac = db.DoiTac.Find(id);
            if (doiTac == null)
            {
                return HttpNotFound();
            }
            return View(doiTac);
        }

        // POST: Admin/DoiTac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DoiTac doiTac = db.DoiTac.Find(id);
            db.DoiTac.Remove(doiTac);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult LoadDS()
        {
            return PartialView("_DoiTacPartial", db.DoiTac.ToList());
        }
        public ActionResult Search(string gender)
        {
            if (!String.IsNullOrEmpty(gender))
            {
                var ds = db.DoiTac.Where
                    (
                    x => x.MaDT.ToUpper().Contains(gender.ToUpper())
                    || x.FullName.ToUpper().Contains(gender.ToUpper())
                    || x.SDT.ToUpper().Contains(gender.ToUpper())
                    );
                return PartialView("_DoiTacPartial", ds.ToList());
            }
            else return PartialView("_DoiTacPartial");
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
