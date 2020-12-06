using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testshop.Models;
using System.IO;

namespace Testshop.Controllers
{
    public class RegisterController : Controller
    {
        TestShopEntities db = new TestShopEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveData(User user)
        {
            user.IsValid = false;
            db.Users.Add(user);
            db.SaveChanges();
            return Json("Register Successfull", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckValidUser(User user)
        {
            string result = "Fail";
            var DataItem = db.Users.Where(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword).SingleOrDefault();
            if (DataItem != null)
            {
                Session["UserID"] = DataItem.IDuser.ToString();
                Session["UserName"] = DataItem.UserName.ToString();
                result = "Success";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AfterLogin()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}