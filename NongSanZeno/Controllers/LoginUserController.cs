using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using NongSanZeno.Models;

namespace NongSanZeno.Controllers
{
    public class LoginUserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}