using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotellWebMvc.Controllers
{
    public class AuthenticationController : Controller
    {
        public ActionResult Login()
        {
            return Content("Login");
        }
    }
}