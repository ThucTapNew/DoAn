using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLiKho.Models.Entities;
using System.IO;

namespace QuanLiKho.Areas.Admin.Controllers
{
    public class HinhAnhController : Controller
    {
        string fileName;
        string path;
        private Entities db = new Entities();

        // GET: Admin/HinhAnh
        public ActionResult Index()
        {
            var hinhAnhs = db.HinhAnh.Include(h => h.HangHoa);
            return View(hinhAnhs.ToList());
        }

        // GET: Admin/HinhAnh/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnh.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnh);
        }

        // GET: Admin/HinhAnh/Create
        public ActionResult Create()
        {
            ViewBag.MaHH = new SelectList(db.HangHoa, "MaHH", "TenHH");
            return View();
        }

        // POST: Admin/HinhAnh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HinhAnh hinhAnh, List<HttpPostedFileBase> file)
        {
            foreach (var item in file)
            {
                if (item.ContentLength > 0)
                {
                    fileName = Path.GetFileName(item.FileName);
                    path = Path.Combine(Server.MapPath("~/Content/Image"), fileName);
                    item.SaveAs(path);
                    if (ModelState.IsValid)
                    {
                        string maIMG = "";
                        if (db.HinhAnh.Count() != 0)
                        {
                            var Nh = (from p in db.HinhAnh
                                      orderby p.MaIMG descending
                                      select p).Skip(0).Take(1);
                            string numberString = Nh.ToList()[0].MaIMG.Substring(3);
                            int number = Convert.ToInt32(numberString);
                            number++;
                            numberString = number.ToString();
                            while (numberString.Length < 5)
                            {
                                numberString = "0" + numberString;
                            }
                            maIMG = "IMG" + numberString;
                        }
                        else
                        {
                            maIMG = "IMG00001";
                        }
                        var result = db.HinhAnh.Count();
                        HinhAnh img = new HinhAnh
                        {
                            MaHH = hinhAnh.MaHH,
                            MaIMG = maIMG + result,
                            TenIMG = fileName,
                            PathFile = string.Join("/", "~/Content/Image", fileName),
                           TinhChat = hinhAnh.TinhChat
                        };
                        db.HinhAnh.Add(img);
                        db.SaveChanges();
                    }
                }

            }
            ViewBag.MaHH = new SelectList(db.HangHoa, "MaHH", "TenHH", hinhAnh.MaHH);
            return View(hinhAnh);
        }

        // GET: Admin/HinhAnh/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnh.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHH = new SelectList(db.HangHoa, "MaHH", "TenHH", hinhAnh.MaHH);
            return View(hinhAnh);
        }

        // POST: Admin/HinhAnh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaIMG,TenIMG,MaHH,Path,TinhChat")] HinhAnh hinhAnh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhAnh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHH = new SelectList(db.HangHoa, "MaHH", "TenHH", hinhAnh.MaHH);
            return View(hinhAnh);
        }

        // GET: Admin/HinhAnh/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnh.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnh);
        }

        // POST: Admin/HinhAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HinhAnh hinhAnh = db.HinhAnh.Find(id);
            db.HinhAnh.Remove(hinhAnh);
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

