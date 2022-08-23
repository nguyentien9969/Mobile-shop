using Base.Entity.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.Controllers
{
    public class BaseController : Controller
    {
        protected new JsonResult Json(object data)
        {
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }

        protected void HandleException(MainResponse mainResponse, Exception exception)
        {
            mainResponse.HandleException(exception);
        }
    }
}