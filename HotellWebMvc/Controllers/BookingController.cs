using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotellWebMvc.Controllers
{
    public class BookingController : Controller
    {
        public ActionResult Index()
        {
            return Content("hei");
        }
    }
}