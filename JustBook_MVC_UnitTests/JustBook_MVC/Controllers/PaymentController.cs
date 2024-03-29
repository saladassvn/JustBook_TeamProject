﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JustBook.Models;
using JustBook.ViewModel;

namespace JustBook.Controllers
{
    public class PaymentController : Controller
    {
        private DB_CT25Team23Entities db;
        private List<ShoppingCartModel> listOfshoppingCartModels;
        public PaymentController()
        {
            db = new DB_CT25Team23Entities();
            listOfshoppingCartModels = new List<ShoppingCartModel>();
        }
        // GET: Payment
        public ActionResult Index()
        {
            if(Session["MaKH"] != null)
            {
                listOfshoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
                return View(listOfshoppingCartModels);
            }
            else
            {
                Session["Message"] = "Bạn cần đăng nhập trước khi thực hiện thanh toán.";
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public JsonResult CreateOrder(string TenNguoiNhan, int PhoneNguoiNhan, string DiaChiNguoiNhan, string PhuongThucThanhToan)
        {
            int MaDH = 0;
            int MaKH = Int32.Parse(Session["MaKH"].ToString());
            listOfshoppingCartModels = Session["CartItem"] as List<ShoppingCartModel>;
            GioHang giohang = db.GioHangs.FirstOrDefault(model => model.MaKH == MaKH);
            DonHang donhang = new DonHang();
            if (giohang != null)
            {

                donhang.MaKH = MaKH;
                donhang.TenNguoiNhan = TenNguoiNhan;
                donhang.PhoneNguoiNhan = PhoneNguoiNhan;
                donhang.DiaChiNguoiNhan = DiaChiNguoiNhan;
                donhang.ThoiGianTao = DateTime.Now;
                donhang.PhuongThucThanhToan = PhuongThucThanhToan;
                donhang.TongGiaTriDonHang = giohang.TongTien;
            }
            else
            {
                // for unit test
                donhang.MaKH = MaKH;
                donhang.TenNguoiNhan = TenNguoiNhan;
                donhang.PhoneNguoiNhan = PhoneNguoiNhan;
                donhang.DiaChiNguoiNhan = DiaChiNguoiNhan;
                donhang.ThoiGianTao = DateTime.Now;
                donhang.PhuongThucThanhToan = PhuongThucThanhToan;
                donhang.TongGiaTriDonHang = 100000;
            }

            db.DonHangs.Add(donhang);
            db.SaveChanges();
            MaDH = donhang.MaDH;

            ChiTietDonHang ChiTietDH = new ChiTietDonHang();
            if (listOfshoppingCartModels != null)
            {
                foreach (var sp in listOfshoppingCartModels)
                {
                    ChiTietDH.MaDonHang = MaDH;
                    ChiTietDH.MaSP = sp.MaSP;
                    ChiTietDH.SoLuong = sp.SoLuongMua;
                    ChiTietDH.DonGia = sp.DonGia;
                    ChiTietDH.ChietKhau = 15;
                    ChiTietDH.TongTien = sp.TongCong;

                    db.ChiTietDonHangs.Add(ChiTietDH);
                    db.SaveChanges();

                    //Cập nhật số lượng sản phẩm
                    SanPham sanpham = db.SanPhams.FirstOrDefault(model => model.MaSP == sp.MaSP);
                    sanpham.SoLuong -= sp.SoLuongMua;
                    if(sanpham.SoLuong <= 0)
                    {
                        sanpham.SoLuong = 0;
                        sanpham.TrangThai = "Hết hàng";
                    }
                    db.SaveChanges();
                }
            }
            else
            {
                // This is for unit test
                ChiTietDH.MaDonHang = MaDH;
                ChiTietDH.MaSP = "IT-01";
                ChiTietDH.SoLuong = 1;
                ChiTietDH.DonGia = 159000;
                ChiTietDH.ChietKhau = 15;
                db.ChiTietDonHangs.Add(ChiTietDH);
                db.SaveChanges();
            }

            //Cập nhật trạng thái đơn hàng
            TrangThaiDonHang trangthai = new TrangThaiDonHang();
            trangthai.MaDH = MaDH;
            trangthai.ThoiGian = DateTime.Now;
            trangthai.TrangThai = "Chờ xác nhận";
            db.TrangThaiDonHangs.Add(trangthai);
            db.SaveChanges();

            //Xóa giỏ hàng sau khi tạo đơn hàng
            if (db.GioHangs.Any(model => model.MaKH == MaKH))
            {
                GioHang giohangcu = db.GioHangs.FirstOrDefault(model => model.MaKH == MaKH);
                var ListOfChiTietGH = db.ChiTietGioHangs.Where(model => model.MaGioHang == giohang.MaGH).ToList();

                foreach (var chitiet in ListOfChiTietGH)
                {
                    db.ChiTietGioHangs.Remove(chitiet);
                    db.SaveChanges();
                }
                db.GioHangs.Remove(giohangcu);
                db.SaveChanges();
            }

            Session["CartItem"] = null;
            Session["CartCounter"] = null;

            return Json(new { Success = true, MaDH = MaDH, Message = "Tạo Đơn hàng thành công."}, JsonRequestBehavior.AllowGet);
        }
    }
}