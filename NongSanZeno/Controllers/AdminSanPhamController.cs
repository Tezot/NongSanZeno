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
        dbNongSanZenoDataContext data = new dbNongSanZenoDataContext();
        // GET: AdminSanPham
        //------------------------------ Sản Phẩm ---------------------------------------

        public ActionResult DSsanpham(int? page)
        {
            if (Session["TKadmin"] == null)
            {
                return RedirectToAction("SanPham", "NongSanZeno");
            }
            int pagesize = 8;
            int pageNum = (page ?? 1);
            var list = data.tbSanPhams.OrderByDescending(s => s.MaSP).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }

        //------------------------------ Chi tiet sản phẩm ---------------------------------------
        public ActionResult Chitietsanpham(int id)
        {
            if (Session["TKadmin"] == null)
            {
                return RedirectToAction("SanPham", "NongSanZeno");
            }


            tbSanPham sp = data.tbSanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpGet]
        public ActionResult Themsanpham()
        {
            if (Session["TKadmin"] == null)
            {
                return RedirectToAction("SanPham", "NongSanZeno");
            }
            ViewBag.Loai = new SelectList(data.tbLoaiSanPhams.ToList().OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.Nhom = new SelectList(data.tbNhoms.ToList().OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
            ViewBag.Dvt = new SelectList(data.tbDonViTinhs.ToList().OrderBy(n => n.MaDVT), "MaDVT", "TenDVT");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themsanpham(tbSanPham sp, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            if (Session["TKadmin"] == null)
            {
                return RedirectToAction("SanPham", "NongSanZeno");
            }
            ViewBag.Loai = new SelectList(data.tbLoaiSanPhams.ToList().OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoaiSP");
            ViewBag.Nhom = new SelectList(data.tbNhoms.ToList().OrderBy(n => n.TenNhom), "MaNhom", "TenNhom");
            ViewBag.Dvt = new SelectList(data.tbDonViTinhs.ToList().OrderBy(n => n.MaDVT), "MaDVT", "TenDVT");
            var tensp = collection["Ten"];
            var gia = collection["Gia"];
            var mota = collection["textarea"];
            var date = collection["Date"];
            var loai = collection["Loai"];
            var nhom = collection["Nhom"];
            var dvt = collection["Dvt"];

            var filename = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/images/sanpham"), filename);
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBaoAnh = "Hình Ảnh Đã Tồn Tại";
                return View();
            }
            else
            {
                fileUpload.SaveAs(path);
            }

            sp.TenSP = tensp;
            sp.GiaBan = decimal.Parse(gia);
            sp.MoTa = mota;
            sp.NgayCapNhat = DateTime.Parse(date);
            sp.MaLoaiSP = Int32.Parse(loai);
            sp.MaNhom = Int32.Parse(nhom);
            sp.MaDVT = Int32.Parse(dvt);
            sp.AnhSP = filename;
            data.tbSanPhams.InsertOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("DSsanpham", "AdminSanPham");
        }

        [HttpGet]
        public ActionResult Xoasanpham(int id)
        {
            if (Session["TKadmin"] == null)
            {
                return RedirectToAction("SanPham", "NongSanZeno");
            }
            else
            {
                tbSanPham sp = data.tbSanPhams.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }
        [HttpPost, ActionName("Xoasanpham")]
        public ActionResult XacNhanXoasanpham(int id)
        {
            if (Session["TKadmin"] == null)
            {
                return RedirectToAction("SanPham", "NongSanZeno");
            }
            else
            {
                tbSanPham sp = data.tbSanPhams.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                data.tbSanPhams.DeleteOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("DSsanpham");
            }
        }
    }
}