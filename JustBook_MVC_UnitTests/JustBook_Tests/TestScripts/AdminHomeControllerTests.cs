using JustBook;
using JustBook.Controllers;
using JustBook.Models;
using JustBook.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Drawing;
using System.Collections.Generic;

namespace JustBook_Tests.TestScripts.AdminHomeControllerTest
{
    [TestClass]
    public class AdminHomeControllerTest
    {

        [TestMethod]
        public void GetIndex_ShouldReturnIndexView()
        {
            var controller = new AdminHomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AdminAccount_ShouldReturnAdminInfo()
        {
            var adminHomeController = new AdminHomeController();
            var controllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            //controllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controllerContext.Setup(p => p.HttpContext.Session["MaQT"]).Returns("4");
            adminHomeController.ControllerContext = controllerContext.Object;
            var result = adminHomeController.AdminAccount() as ViewResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void OrderManagement_ShouldReturnOrdersList()
        {
            var adminHomeController = new AdminHomeController();
            string searching = "Chờ xác nhận";
            var result = adminHomeController.OrderManagement(searching);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void getInfo_ShouldReturnRIghtProductDetail()
        {
            var controller = new AdminHomeController();
            var result = controller.getInfo("IT-02");
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }

        [TestMethod]
        public void OrderDetail_ShouldReturnDetailOfOrder()
        {
            var controller = new AdminHomeController();
            int idDH = 41;
            var result = controller.OrderDetail(idDH) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AddProduct_ShouldReturnViewOfAddProduct()
        {
            var controller = new AdminHomeController();
            var result = controller.AddProduct() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AddProduct_WithCorrectInputValue_ShouldAddProductSuccessful()
        {
            var controller = new AdminHomeController();

            //Thiết lập path và thuộc tính của Image
            var contentType = "image/jpg";
            var fileName = "123.jpg";
            string path = "C:\\Users\\Cong\\Pictures\\123.jpg";
            Image img = Image.FromFile(path);

            //ImageConverter Class convert Image object to Byte array.
            byte[] byteArray = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
            MemoryStream stream = new MemoryStream(byteArray);
            MemoryFile imageFile = new MemoryFile(stream, contentType, fileName);

            SanPhamViewModel sp_viewmodel = new SanPhamViewModel
            {
                MaSP = "add-test",
                MaLoaiSP = 2,
                TenSP = "test sách",
                TacGia = "tester",
                NXB = "HCM",
                DonGia = 109000,
                MoTa = "Test add hình",
                SoLuong = 12,
                SoTrang = 123,
                TrongLuong = "2",
                KichThuoc = "12",
                LoaiBia = "Bìa cứng",
                ImagePath = imageFile
            };

            var imageName = sp_viewmodel.MaSP + "_" + DateTime.Now.ToFileTime() + Path.GetExtension(sp_viewmodel.ImagePath.FileName);
            sp_viewmodel.ImageName = imageName;

            //create mock of HttpServerUtilityBase
            var server = new Mock<HttpServerUtilityBase>();
            var httpContextMock = new Mock<HttpContextBase>();

            //set up mock to return known value on call.
            server.Setup(x => x.MapPath("~/ImageProduct/")).Returns("D:\\JustBook_UnitTest\\JustBook_TeamProject\\JustBook_MVC_UnitTests\\JustBook_MVC\\ImageProduct");
            server.Setup(x => x.MapPath("~/ImageProduct/" + imageName)).Returns("D:\\JustBook_UnitTest\\JustBook_TeamProject\\JustBook_MVC_UnitTests\\JustBook_MVC\\ImageProduct\\" + imageName);
            httpContextMock.Setup(x => x.Server).Returns(server.Object);

            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controller.ControllerContext = mock.Object;

            // Action
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.AddProduct(sp_viewmodel) as JsonResult;

            // Assert
            dynamic jsonResult = result.Data;
            var success_data = result.Data.GetType().GetProperty("Success").GetValue(jsonResult, null);
            var message_data = result.Data.GetType().GetProperty("Message").GetValue(jsonResult, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, success_data);
            Assert.AreEqual("Sản phẩm đã được thêm mới thành công.", message_data);
        }
        
        
        [TestMethod]
        public void AddProduct_WithExistID_ShouldAddProductUnsuccessful()
        {
            var controller = new AdminHomeController();

            //Thiết lập path và thuộc tính của Image
            var contentType = "image/jpg";
            var fileName = "123.jpg";
            string path = "C:\\Users\\Cong\\Pictures\\123.jpg";
            Image img = Image.FromFile(path);

            //ImageConverter Class convert Image object to Byte array.
            byte[] byteArray = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
            MemoryStream stream = new MemoryStream(byteArray);
            MemoryFile imageFile = new MemoryFile(stream, contentType, fileName);

            SanPhamViewModel sp_viewmodel = new SanPhamViewModel
            {
                MaSP = "test",
                MaLoaiSP = 2,
                TenSP = "test sách",
                TacGia = "tester",
                NXB = "HCM",
                DonGia = 109000,
                MoTa = "Test add hình",
                SoLuong = 12,
                SoTrang = 123,
                TrongLuong = "2",
                KichThuoc = "12",
                LoaiBia = "Bìa cứng",
                ImagePath = imageFile
            };

            var imageName = sp_viewmodel.MaSP + "_" + DateTime.Now.ToFileTime() + Path.GetExtension(sp_viewmodel.ImagePath.FileName);
            sp_viewmodel.ImageName = imageName;

            //create mock of HttpServerUtilityBase
            var server = new Mock<HttpServerUtilityBase>();
            var httpContextMock = new Mock<HttpContextBase>();

            //set up mock to return known value on call.
            server.Setup(x => x.MapPath("~/ImageProduct/")).Returns("D:\\JustBook_UnitTest\\JustBook_TeamProject\\JustBook_MVC_UnitTests\\JustBook_MVC\\ImageProduct");
            server.Setup(x => x.MapPath("~/ImageProduct/" + imageName)).Returns("D:\\JustBook_UnitTest\\JustBook_TeamProject\\JustBook_MVC_UnitTests\\JustBook_MVC\\ImageProduct\\" + imageName);
            httpContextMock.Setup(x => x.Server).Returns(server.Object);

            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controller.ControllerContext = mock.Object;

            // Action
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.AddProduct(sp_viewmodel) as JsonResult;

            // Assert
            dynamic jsonResult = result.Data;
            var success_data = result.Data.GetType().GetProperty("Success").GetValue(jsonResult, null);
            var message_data = result.Data.GetType().GetProperty("Message").GetValue(jsonResult, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(false, success_data);
            Assert.AreEqual("Mã sản phẩm đã tồn tại", message_data);
        }

        [TestMethod]
        public void AddProduct_WithNullInputValue_ShouldAddProductUnsuccessful()
        {
            var controller = new AdminHomeController();

            SanPhamViewModel sp_viewmodel = new SanPhamViewModel
            {
               
            };

            // Action
 
            var result = controller.AddProduct(sp_viewmodel) as JsonResult;

            // Assert
            dynamic jsonResult = result.Data;
            var success_data = result.Data.GetType().GetProperty("Success").GetValue(jsonResult, null);
            var message_data = result.Data.GetType().GetProperty("Message").GetValue(jsonResult, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(false, success_data);
            Assert.AreEqual("Vui lòng nhập đầy đủ thông tin.", message_data);
        }

        [TestMethod]
        public void DeleteProduct_ShouldDeleteProductSuccessful()
        {
            var controller = new AdminHomeController();
            string MaSP = "add-test";

            //create mock of HttpServerUtilityBase
            var server = new Mock<HttpServerUtilityBase>();
            var httpContextMock = new Mock<HttpContextBase>();

            //set up mock to return known value on call.
            server.Setup(x => x.MapPath("~/ImageProduct/")).Returns("D:\\JustBook_UnitTest\\JustBook_TeamProject\\JustBook_MVC_UnitTests\\JustBook_MVC\\ImageProduct");
            httpContextMock.Setup(x => x.Server).Returns(server.Object);

            // Action
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.DeleteProduct(MaSP) as JsonResult;

            // Assert
            dynamic jsonResult = result.Data;
            var success_data = result.Data.GetType().GetProperty("Success").GetValue(jsonResult, null);
            var message_data = result.Data.GetType().GetProperty("Message").GetValue(jsonResult, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, success_data);
            Assert.AreEqual("Xóa sản phẩm #" + MaSP + " thành công.", message_data);
        }

        [TestMethod]
        public void EditProduct_ShouldReturnDetailOfProduct()
        {
            var controller = new AdminHomeController();
            var MaSP = "TA-02"; 
            var result = controller.EditProduct(MaSP) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void EditProduct_WithCorrectInputValueAndChangeImage_ShouldEditProductSuccessful()
        {
            var controller = new AdminHomeController();

            //Thiết lập path và thuộc tính của Image
            var contentType = "image/png";
            var fileName = "book.png";
            string path = "C:\\JustBook_UnitTest\\JustBook_TeamProject\\JustBook_MVC_UnitTests\\JustBook_MVC\\images\\book.png";
            Image img = Image.FromFile(path);

            //ImageConverter Class convert Image object to Byte array.
            byte[] byteArray = (byte[])(new ImageConverter()).ConvertTo(img, typeof(byte[]));
            MemoryStream stream = new MemoryStream(byteArray);
            MemoryFile imageFile = new MemoryFile(stream, contentType, fileName);

            SanPhamViewModel sp_viewmodel = new SanPhamViewModel
            {
                MaSP = "test",
                MaLoaiSP = 2,
                TenSP = "test sách",
                TacGia = "tester",
                NXB = "HCM",
                DonGia = 109000,
                MoTa = "Test có thay đổi hình",
                SoLuong = 12,
                SoTrang = 123,
                TrongLuong = "2",
                KichThuoc = "12",
                LoaiBia = "Bìa cứng",
                ImagePath = imageFile
            };

            var imageName = sp_viewmodel.MaSP + "_" + DateTime.Now.ToFileTime() + Path.GetExtension(sp_viewmodel.ImagePath.FileName);
            sp_viewmodel.ImageName = imageName;

            //create mock of HttpServerUtilityBase
            var server = new Mock<HttpServerUtilityBase>();
            var httpContextMock = new Mock<HttpContextBase>();

            //set up mock to return known value on call.
            server.Setup(x => x.MapPath("~/ImageProduct/")).Returns("C:\\JustBook_UnitTest\\JustBook_TeamProject\\JustBook_MVC_UnitTests\\JustBook_MVC\\ImageProduct");
            server.Setup(x => x.MapPath("~/ImageProduct/" + imageName)).Returns("C:\\JustBook_UnitTest\\JustBook_TeamProject\\JustBook_MVC_UnitTests\\JustBook_MVC\\ImageProduct\\" + imageName);
            httpContextMock.Setup(x => x.Server).Returns(server.Object);

            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controller.ControllerContext = mock.Object;

            // Action
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);
            var result = controller.EditProduct(sp_viewmodel) as JsonResult;

            // Assert
            dynamic jsonResult = result.Data;
            var success_data = result.Data.GetType().GetProperty("Success").GetValue(jsonResult, null);
            var message_data = result.Data.GetType().GetProperty("Message").GetValue(jsonResult, null);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(true, success_data);
            Assert.AreEqual("Cập nhật sản phẩm thành công.", message_data);
        }

        [TestMethod]
        public void EditProduct_WithCorrectInputValueAndNotChangeImage_ShouldEditProductSuccessful()
        {
            var controller = new AdminHomeController();

            SanPhamViewModel sp_viewmodel = new SanPhamViewModel
            {
                MaSP = "test",
                MaLoaiSP = 2,
                TenSP = "test sách",
                TacGia = "tester",
                NXB = "HCM",
                DonGia = 109000,
                MoTa = "Test không thay đổi hình",
                SoLuong = 12,
                SoTrang = 123,
                TrongLuong = "2",
                KichThuoc = "12",
                LoaiBia = "Bìa cứng"
            };

            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controller.ControllerContext = mock.Object;

            // Action
            var result = controller.EditProduct(sp_viewmodel) as JsonResult;

            // Assert
            dynamic jsonResult = result.Data;
            var success_data = result.Data.GetType().GetProperty("Success").GetValue(jsonResult, null);
            var message_data = result.Data.GetType().GetProperty("Message").GetValue(jsonResult, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, success_data);
            Assert.AreEqual("Cập nhật sản phẩm thành công.", message_data);
        }

        [TestMethod]
        public void EditProduct_WithNullInputValueAndNotChangeImage_ShouldEditProductUnsuccessful()
        {
            var controller = new AdminHomeController();

            SanPhamViewModel sp_viewmodel = new SanPhamViewModel
            {

            };

            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controller.ControllerContext = mock.Object;

            // Action
            var result = controller.EditProduct(sp_viewmodel) as JsonResult;

            // Assert
            dynamic jsonResult = result.Data;
            var success_data = result.Data.GetType().GetProperty("Success").GetValue(jsonResult, null);
            var message_data = result.Data.GetType().GetProperty("Message").GetValue(jsonResult, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(false, success_data);
            Assert.AreEqual("Vui lòng nhập đầy đủ thông tin.", message_data);
        }

    }
}
