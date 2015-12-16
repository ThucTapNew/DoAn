using QuanLiKho.Areas.Admin.Models;
using QuanLiKho.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuanLiKho.Areas.Inventory.Controllers
{
    public class DoiTacController : Controller
    {
        private Entities db = new Entities();


        // GET: Inventory/DoiTac
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

    }
}