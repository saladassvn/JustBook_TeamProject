using System;
using JustBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JustBook.Controllers;
using JustBook.Models;
using System.Web.Mvc;
using Moq;


namespace JustBook_Tests.TestScripts
{
    [TestClass]
    public class RegisterControllerTests
    {
        [TestMethod]
        public void Index_WithCorrectInputValue_ShouldRegisterSucessful()
        {
            // Arange
            RegisterController registerController = new RegisterController();
            DateTime birth = new DateTime(2018, 7, 24);
            TaiKhoanKH model = new TaiKhoanKH()
            {
                //Sau khi chay unit test nay roi thi phai thay doi email thanh email khac
                Email = "ta23@gmail.com",
                MatKhau = "123456",
                XacNhanMatKhau = "123456",
                TenKH = "Bui Van Dat",
                DiaChi = "123123 hung vuong",
                Phone = 59113123,
                GioiTinh = "nam",
                NgaySinh = birth
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            registerController.ControllerContext = mock.Object;
            // Action
            var result = registerController.Index(model) as RedirectToRouteResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult)); ;
        }
        [TestMethod]
        public void Index_WithEmailAlreadyExist_ShouldReturnBackToView()
        {
            // Arange
            RegisterController registerController = new RegisterController();
            DateTime birth = new DateTime(2018, 7, 24);
            TaiKhoanKH model = new TaiKhoanKH()
            {
                Email = "dat123@gmail.com",
                MatKhau = "123456",
                XacNhanMatKhau = "123456",
                TenKH = "Le Van Dat",
                DiaChi = "123123 hung vuong",
                Phone = 59113123,
                GioiTinh = "nam",
                NgaySinh = birth
            };
            // Action
            var result = registerController.Index(model) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
