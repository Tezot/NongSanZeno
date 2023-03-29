using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using NongSanZeno.Models;
using PagedList;
using System.Configuration;
using System.Data.SqlClient;

namespace NongSanZeno.Controllers
{
    public class AdminSanPhamController : Controller
    {
        public ActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var chitietSP = (from s in data.tbSanPhams where s.MaSP == id select s).Single();
            ViewBag.Description = chitietSP.MoTa;
            return View(chitietSP);
        }
    }
}