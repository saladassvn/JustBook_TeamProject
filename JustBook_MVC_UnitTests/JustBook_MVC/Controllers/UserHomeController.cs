﻿using JustBook.Models;
using JustBook.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace JustBook.Controllers
{
    public class UserHomeController : Controller
    {
        private DB_CT25Team23Entities db;
        private List<OrderDetailViewModel> listOfDetail;
        private List<OrderStateViewModel> listOfState;
        public UserHomeController()
        {
            db = new DB_CT25Team23Entities();
            listOfDetail = new List<OrderDetailViewModel>();
            listOfState = new List<OrderStateViewModel>();
        }
        public ActionResult Index()
        {
            if (Session["MaKH"] == null)
            {
                return RedirectToAction("Verify", "Login");
            }
            int makh = Int32.Parse(Session["MaKH"].ToString());
            var user = db.TaiKhoanKHs.FirstOrDefault(ad => ad.MaKH == makh);

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEditInformation(TaiKhoanKH user)
        {
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var users = db.TaiKhoanKHs.FirstOrDefault(ad => ad.MaKH == user.MaKH);
            if (users == null)
            {
                return RedirectToAction("Index");
            }
            users.GioiTinh = user.GioiTinh;
            users.DiaChi = user.DiaChi;
            users.TenKH = user.TenKH;
            users.Email = user.Email;
            users.NgaySinh = user.NgaySinh;
            users.Phone = user.Phone;
            users.XacNhanMatKhau = users.MatKhau;
            db.SaveChanges();
            Session["TenKH"] = user.TenKH;
            return RedirectToAction("Index");
        }

        public ActionResult Notification()
        {
            int idKH = Int32.Parse(Session["MaKH"].ToString());
            IEnumerable<OrderManagementModel> listOfDonHang = (from trangthai in
               (from trangthai in db.TrangThaiDonHangs
                group trangthai by trangthai.MaDH into grp
                select grp.OrderByDescending(x => x.MaTrangThaiDH).FirstOrDefault())
                    join dh in db.DonHangs on trangthai.MaDH equals dh.MaDH
                    join chitiet in db.ChiTietDonHangs on dh.MaDH equals chitiet.MaDonHang
                    join sp in db.SanPhams on chitiet.MaSP equals sp.MaSP
                    where dh.MaKH == idKH
                    orderby dh.MaDH descending
                    select new OrderManagementModel()
                    {
                        MaDH = dh.MaDH,
                        ThoiGianTao = dh.ThoiGianTao
                    }
               ).GroupBy(x => x.MaDH).Select(i => i.FirstOrDefault()).ToList();
            var listAfterDescending = listOfDonHang.OrderByDescending(x => x.MaDH).ToList();
            return View(listAfterDescending);
        }

        public ActionResult OrderHistory()
        {
            int idKH = Int32.Parse(Session["MaKH"].ToString());
            IEnumerable<OrderManagementModel> listOfDonHang = (from trangthai in
               (from trangthai in db.TrangThaiDonHangs
                group trangthai by trangthai.MaDH into grp
                select grp.OrderByDescending(x => x.MaTrangThaiDH).FirstOrDefault())
                    join dh in db.DonHangs on trangthai.MaDH equals dh.MaDH
                    join chitiet in db.ChiTietDonHangs on dh.MaDH equals chitiet.MaDonHang
                    join sp in db.SanPhams on chitiet.MaSP equals sp.MaSP
                    where dh.MaKH == idKH
                    select new OrderManagementModel()
                    {
                        MaDH = dh.MaDH,
                        MaKH = dh.MaKH,
                        TenSP = sp.TenSP,
                        TenNguoiNhan = dh.TenNguoiNhan,
                        PhoneNguoiNhan = dh.PhoneNguoiNhan,
                        DiaChiNguoiNhan = dh.DiaChiNguoiNhan,
                        ThoiGianTao = dh.ThoiGianTao,
                        PhuongThucThanhToan = dh.PhuongThucThanhToan,
                        TongGiaTriDonHang = dh.TongGiaTriDonHang,
                        TrangThaiDonHang = trangthai.TrangThai
                    }
               ).GroupBy(x => x.MaDH).Select(i => i.FirstOrDefault()).ToList();
            var listAfterDescending = listOfDonHang.OrderByDescending(x => x.MaDH).ToList();
            return View(listAfterDescending);
        }

        public ActionResult OrderUserDetail(int id)
        {
            var currentId_Url = id;

            OrderManagementModel dh_model_url = new OrderManagementModel();
            DonHang dh = db.DonHangs.SingleOrDefault(model => model.MaDH.ToString() == currentId_Url.ToString());
            TrangThaiDonHang trangthai = db.TrangThaiDonHangs.OrderByDescending(x => x.MaTrangThaiDH).FirstOrDefault(model => model.MaDH == dh.MaDH);

            var ListOfChiTietDH = db.ChiTietDonHangs.Where(model => model.MaDonHang == dh.MaDH).ToList();
            foreach (var chitiet in ListOfChiTietDH)
            {
                OrderDetailViewModel detail = new OrderDetailViewModel();
                SanPham sanpham = db.SanPhams.FirstOrDefault(x => x.MaSP == chitiet.MaSP);

                detail.MaChiTietDH = chitiet.MaChiTietDH;
                detail.MaDonHang = dh.MaDH;
                detail.MaSP = chitiet.MaSP;
                detail.SoLuong = chitiet.SoLuong;
                detail.SoLuongConLai = sanpham.SoLuong;
                detail.DonGia = chitiet.DonGia;
                detail.ChietKhau = chitiet.ChietKhau;
                detail.TongTien = chitiet.TongTien;
                detail.TenSP = sanpham.TenSP;
                detail.LoaiSanPham = db.LoaiSanPhams.FirstOrDefault(x => x.MaLoaiSP == sanpham.MaLoaiSP).TenLoaiSP;
                detail.ImagePath = sanpham.ImagePath;

                listOfDetail.Add(detail);
            }

            dh_model_url.MaDH = dh.MaDH;
            dh_model_url.MaKH = dh.MaKH;
            dh_model_url.TenNguoiNhan = dh.TenNguoiNhan;
            dh_model_url.PhoneNguoiNhan = dh.PhoneNguoiNhan;
            dh_model_url.DiaChiNguoiNhan = dh.DiaChiNguoiNhan;
            dh_model_url.ThoiGianTao = dh.ThoiGianTao;
            dh_model_url.PhuongThucThanhToan = dh.PhuongThucThanhToan;
            dh_model_url.TongGiaTriDonHang = dh.TongGiaTriDonHang;
            dh_model_url.TrangThaiDonHang = trangthai.TrangThai;
            dh_model_url.ChiTietDonHang = listOfDetail;

            return View(dh_model_url);
        }

        public ActionResult TrackingState(int id)
        {
            var currentId_Url = id;

            OrderManagementModel dh_model_url = new OrderManagementModel();
            DonHang dh = db.DonHangs.SingleOrDefault(model => model.MaDH.ToString() == currentId_Url.ToString());
            TrangThaiDonHang trangthai = db.TrangThaiDonHangs.OrderByDescending(x => x.MaTrangThaiDH).FirstOrDefault(model => model.MaDH == dh.MaDH);

            var ListOfChiTietDH = db.ChiTietDonHangs.Where(model => model.MaDonHang == dh.MaDH).ToList();
            foreach (var chitiet in ListOfChiTietDH)
            {
                OrderDetailViewModel detail = new OrderDetailViewModel();
                SanPham sanpham = db.SanPhams.FirstOrDefault(x => x.MaSP == chitiet.MaSP);

                detail.MaChiTietDH = chitiet.MaChiTietDH;
                detail.MaDonHang = dh.MaDH;
                detail.MaSP = chitiet.MaSP;
                detail.TenSP = sanpham.TenSP;
                detail.ImagePath = sanpham.ImagePath;

                listOfDetail.Add(detail);
            }

            var ListOfTrangThaiDH = db.TrangThaiDonHangs.Where(model => model.MaDH == dh.MaDH).ToList();
            foreach (var trangthai_db in ListOfTrangThaiDH)
            {
                OrderStateViewModel state = new OrderStateViewModel();
                state.MaDH = dh.MaDH;
                state.MaTrangThaiDH = trangthai_db.MaTrangThaiDH;
                state.ThoiGian = trangthai_db.ThoiGian;
                state.TrangThai = trangthai_db.TrangThai;

                listOfState.Add(state);
            }

            dh_model_url.MaDH = dh.MaDH;
            dh_model_url.ThoiGianTao = dh.ThoiGianTao;
            dh_model_url.TrangThaiDonHang = trangthai.TrangThai;
            dh_model_url.ChiTietDonHang = listOfDetail;
            dh_model_url.listOfState = listOfState;

            return View(dh_model_url);
        }

    }
}
