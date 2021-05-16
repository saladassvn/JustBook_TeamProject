using JustBook.Models;
using JustBook.ViewModel;
using System.Web.Mvc;

namespace JustBook.Controllers
{
    public interface IAdminHomeRepo
    {
        ActionResult AddProduct();
        JsonResult AddProduct(SanPhamViewModel sp_viewmodel);
        ActionResult AdminAccount();
        ActionResult AdminEditInformation(TaiKhoanQT admin);
        ActionResult AdminNotification();
        JsonResult CancelOrder(int MaDH, string TrangThaiDonHang);
        JsonResult DeleteOrder(int MaDH);
        JsonResult GetInfo(string SelectedID);
        ActionResult Index();
        ActionResult OrderDetail();
        ActionResult OrderManagement();
        JsonResult OrderSaveChanges(int MaDH, string MaSP, string SoLuongMua, string TongTienMonHang, double TongCong, string RemoveId, string TrangThaiDonHang);
        ActionResult ProductManagement();
    }
}