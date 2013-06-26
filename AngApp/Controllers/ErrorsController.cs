using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngApp.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Http404()
        {
            ViewBag.ErrorCode = "404";
            ViewBag.ErrorMessage = "Page does not exist";
            return View("Error");
        }

        public ActionResult Http401()
        {
            ViewBag.ErrorCode = "401";
            ViewBag.ErrorMessage = "Page not authorized";
            return View("Error");
        }

        public ActionResult Http500()
        {
            ViewBag.ErrorCode = "500";
            ViewBag.ErrorMessage = "Sorry bro, system failed";
            return View("Error");
        }
    }
}
