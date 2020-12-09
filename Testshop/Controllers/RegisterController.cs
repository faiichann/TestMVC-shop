using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testshop.Models;
using System.IO;
using System.Dynamic;
using Newtonsoft.Json;

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
            //string filename = Path.GetFileNameWithoutExtension(user.ImgUrl.FileName);
            //string extension = Path.GetExtension(user.ImgUrl.FileName);
            //filename = filename + DateTime.Now.ToString("yymmssff") + extension;
            //user.UserImg = filename;
            //user.ImgUrl.SaveAs(Path.Combine(Server.MapPath("~/Images/Profile"), filename));
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
                Session["UserImg"] = DataItem.UserImg.ToString();
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
        public ActionResult Data()
        {

            if (Session["UserID"] != null)
            {
                List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(10, 22),
                new DataPoint(20, 36),
                new DataPoint(30, 42),
                new DataPoint(40, 51),
                new DataPoint(50, 46),
            };

                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}