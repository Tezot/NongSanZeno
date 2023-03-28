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
    public class HomeController : Controller
    {
        dbNongSanZenoDataContext data = new dbNongSanZenoDataContext();

        private List<tbSanPham> Laysanpham(int count)
        {
            return data.tbSanPhams.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult TrangChu()
        {
            //Lay top 6 san pham ban chay nhat
            var sanpham = Laysanpham(42);
            string s = Request.QueryString["s"];
            if (!string.IsNullOrEmpty(s)) sanpham = data.tbSanPhams.OrderByDescending(a => a.NgayCapNhat).Take(42).Where(w => w.TenSP.Contains(s)).ToList();
            return View(sanpham);
        }

        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult TinTuc()
        {
            return View();
        }

        public ActionResult AnToanThucPham()
        {
            return View();
        }

        public ActionResult LienHe()
        {
            return View();
        }
    }
}