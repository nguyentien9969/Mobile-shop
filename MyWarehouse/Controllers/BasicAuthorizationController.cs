using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWarehouse.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BasicAuthorizationController : BaseController
    {

    }
}