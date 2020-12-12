using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace Testshop.ViewModel
{
    public class ItemModel
    {
        public Guid ItemID { get; set; }
        public int CartID { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemDes { get; set; }
        public HttpPostedFileBase ItemImg { get; set; }
        public decimal ItemPrice { get; set; }
        public IEnumerable<SelectListItem> CartListItem { get; set; }
    }
}