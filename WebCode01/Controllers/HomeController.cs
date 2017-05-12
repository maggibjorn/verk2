using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCode01.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Action returns the home page.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        
    }
}