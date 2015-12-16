using QuanLiKho.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiKho.Areas.Admin.Controllers
{
    public class ToDayReportController : Controller
    {
        Entities db = new Entities();
        // GET: Admin/ToDayReport
        public ActionResult Index()
        {
            return View(db.CT_HoaDon.ToList());
        }
      
    }
}