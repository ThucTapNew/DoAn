using QuanLiKho.Areas.Common;
using QuanLiKho.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiKho.Areas.Sale.Controllers
{
    public class SaleController : Controller
    {

        private Entities db = new Entities();
        // GET: Sale/Sale
        public ActionResult Index()
        {

            var a = from p in db.HinhAnh
                    join q in db.HangHoa
                    on p.MaHH equals q.MaHH
                    where p.TinhChat == true
                    select new HHImage
                    {
                        Images = p.PathFile,
                        Gia = q.GiaBan,
                        id = q.MaHH,
                        name = q.TenHH
                    };
            return View(a.ToList());
        }

    }
}