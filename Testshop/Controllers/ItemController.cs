using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testshop.Models;
using Testshop.ViewModel;
using System.IO;

namespace Testshop.Controllers
{
    public class ItemController : Controller
    {
        private TestShopEntities1 db;
    public ItemController()
        {
            db = new TestShopEntities1();
        }
        // GET: Item
        public ActionResult Index()
        {
            ItemModel tbItem = new ItemModel();
            tbItem.CartListItem = (from e in db.Carts
                                   select new SelectListItem()
                                   {
                                          Text = e.CartName,
                                          Value = e.CartID.ToString(),
                                           Selected = true
                                    });
            ViewBag.type = tbItem.CartListItem;
            return View(tbItem);
        }
        [HttpPost]
        public JsonResult Index(ItemModel itemModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(itemModel.ItemImg.FileName);
            itemModel.ItemImg.SaveAs(filename: Server.MapPath("~/Images/Product" + NewImage));

            Item dbItem = new Item();
            dbItem.ItemImg = "~/Images/Product" + NewImage;
            dbItem.CartID = itemModel.CartID;
            dbItem.ItemDes= itemModel.ItemDes;
            dbItem.ItemCode = itemModel.ItemCode;
            dbItem.ItemID = Guid.NewGuid();
            dbItem.ItemName = itemModel.ItemName;
            dbItem.ItemPrice = itemModel.ItemPrice;
            db.Items.Add(dbItem);
            db.SaveChangesAsync();

            return Json(data: new { Success = true, Message = " Add Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}