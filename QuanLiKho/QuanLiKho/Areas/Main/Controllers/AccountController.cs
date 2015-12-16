using QuanLiKho.Areas.Admin.Models;
using QuanLiKho.Areas.Common;
using QuanLiKho.Areas.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiKho.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public static string id_NV;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public string GetId(string id)
        {
            return id_NV = id;
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var db = new KhoDb();
            var result = db.Login(model.Name, Encryptor.MD5Hash(model.Pwd), false);
            if (!ModelState.IsValid)
            {
                if (result == 1)
                {
                    var user = db.GetByID(model.Name);
                    var userSession = new Common.UserLogin();
                    userSession.UserName = user.TenTK;
                    var list = db.GetListCredential(model.Name);
                    Session.Add(Common.Common.SESSION_CREDENTIAL, list);
                    Session.Add(Common.Common.USER_SESSION, userSession);
                    GetId(model.Name);
                    return RedirectToAction("Index", "Sale", new { area = "Sale" });
                }
                else
                {
                    if (result == -1)
                        ModelState.AddModelError("", "Nhập sai password");
                    else
                    {
                        if (result == -2)
                            ModelState.AddModelError("", "Bạn không có quyền đăng nhập");
                    }

                }

            }
            return View();
        }
    }
}