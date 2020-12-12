using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Testshop.ViewModel
{
    public class UserModel
    {
        public int IDuser { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string UserImg { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public HttpPostedFileBase ImgUrl { get; set; }
    }
}