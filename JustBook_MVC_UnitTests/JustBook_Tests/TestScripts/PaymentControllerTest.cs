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
    public class PaymentControllerTest
    {
        [TestMethod]
        public void Index_withMaKHequaNotNull_ReturnPay()
        {
            // Arange
            var pay = new PaymentController();
            var controllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            //controllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controllerContext.Setup(p => p.HttpContext.Session["MaKH"]).Returns("1");
            pay.ControllerContext = controllerContext.Object;
            // Action
            var result = pay.Index() as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void Index_withMaKHequaNull_ReturnLogin()
        {
            // Arange
            var pay = new PaymentController();
            var controllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            //controllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            controllerContext.Setup(p => p.HttpContext.Session["MaKH"]).Returns(null);
            pay.ControllerContext = controllerContext.Object;
            // Action
            var result = pay.Index() as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void Index_WithCorrectInputValue_ShouldSearchSucessful()
        {
            // Arange
            PaymentController pay = new PaymentController();
            DonHang donHang = new DonHang()
            {
                MaKH = 1,
                TenNguoiNhan = "Nguyễn Chí Thành",
                PhoneNguoiNhan = 123456789,
                DiaChiNguoiNhan = "TP.HCM",
                PhuongThucThanhToan = "Tiền mặt"
            };
            // Action
            var result = pay.CreateOrder() as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}

                     