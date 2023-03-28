using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NongSanZeno.Models;
using PagedList;
using PagedList.Mvc;

namespace NongSanZeno.Controllers
{
    public class NongSanZenoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
    }
}