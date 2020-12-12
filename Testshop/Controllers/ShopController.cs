using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testshop.Models;
using Testshop.ViewModel;
using System.IO;
using System.Threading.Tasks;

namespace Testshop.Controllers
{
    public class ShopController : Controller
    {
        private TestShopEntities1 db = new TestShopEntities1();
        private List<ShoppingCartModel> listCart = new List<ShoppingCartModel>();
        // GET: Shop
        public ActionResult Index()
        {
            IEnumerable<ShoppingModel> listStore = (from e in db.Items
                                                                       join s in db.Carts
                                                                       on e.CartID equals s.CartID
                                                                       select new ShoppingModel()
                                                                       {
                                                                           Image = e.ItemImg,
                                                                           Name = e.ItemName,
                                                                           Description = e.ItemDes,
                                                                           Price = e.ItemPrice,
                                                                           ItemId = e.ItemID,
                                                                           Category = s.CartName,
                                                                           ItemCode = e.ItemCode

                                                                       }).ToList();

            return View(listStore);

        }
        [HttpPost]
        public JsonResult Index(string ItemID)
        {
            ShoppingCartModel cartModel = new ShoppingCartModel();
            Item dbItem = db.Items.Single(model => model.ItemID.ToString() == ItemID);
            if (Session["CartCounter"] != null)
            {
                listCart = Session["cartitem"] as List<ShoppingCartModel>;
            }
            if (listCart.Any(Model => Model.ItemId== ItemID))
            {
                cartModel = listCart.Single(model => model.ItemId == ItemID);
                cartModel.Quantity = cartModel.Quantity + 1;
                cartModel.Total = cartModel.Quantity * cartModel.UnitPrice;
            }
            else
            {
                cartModel.ItemId = ItemID;
                cartModel.Image = dbItem.ItemImg;
                cartModel.ItemName = dbItem.ItemName;
                cartModel.Quantity = 1;
                cartModel.Total = dbItem.ItemPrice;
                cartModel.UnitPrice = dbItem.ItemPrice;
                listCart.Add(cartModel);

            }
            Session["CartCounter"] = listCart.Count;
            Session["cartitem"] = listCart;

            return Json(data: new { Success = true, Counter = listCart.Count }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Order()
        {
            listCart = Session["cartitem"] as List<ShoppingCartModel>;

            return View(listCart);
        }
        public ActionResult Delete(string id)
        {
            List<ShoppingCartModel> cart = (List<ShoppingCartModel>)Session["cartitem"];
            var del = cart.Find(m => m.ItemId == id);
            cart.Remove(del);
            Session["CartCounter"] = cart.Count;
            Session["cartitem"] = cart;

            return View("Order", cart);
        }
    }
}