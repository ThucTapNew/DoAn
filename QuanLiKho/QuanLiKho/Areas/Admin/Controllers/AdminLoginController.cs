using QuanLiKho.Areas.Common;
using QuanLiKho.Areas.Admin.Models;
using QuanLiKho.Areas.Main.Models;
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
    public class AdminLoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var db = new KhoDb();
            var result = db.LoginAdmin(model.Name, Encryptor.MD5Hash(model.Pwd), true);
            if (!ModelState.IsValid)
            {
                if (result == 1)
                {
                    var user = db.GetByIDAdmin(model.Name);
                    var userSession = new Common.UserLogin();
                    userSession.UserName = user.TenTK;
                    var list = db.GetListCredentialbyAdmin(model.Name);
                    Session.Add(Common.Common.SESSION_CREDENTIAL, list);
                    Session.Add(Common.Common.USER_SESSION, userSession);
                    return RedirectToAction("index", "GiaoDich", new { area = "Admin" });
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