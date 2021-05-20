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
        [TestClass]
        public class PaymentControllerTests
        {
            private DB_CT25Team23Entities db;
            public PaymentControllerTests()
            {
                db = new DB_CT25Team23Entities();

            }
            [TestMethod]
            public void CreateOrder_ShouldCreateOrderSucessful()
            {
                // Arange
                PaymentController paymentController = new PaymentController();
                string tenNguoiNhan = "Bui Quoc Dat";
                int phoneNguoiNhan = 59113;
                string diaChiNguoiNhan = "123 hung vuong";
                string phuongThucThanhToan = "Tiền mặt";

                var mockControllerContext = new Mock<ControllerContext>();
                var mockSession = new Mock<System.Web.HttpSessionStateBase>();
                // 15 o day la ma khach hang trong database ung voi khach hang bui quoc dat
                mockSession.SetupGet(s => s["MaKH"]).Returns("15");
                mockControllerContext.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
                paymentController.ControllerContext = mockControllerContext.Object;
                // Action
                var result = paymentController.CreateOrder(tenNguoiNhan, phoneNguoiNhan, diaChiNguoiNhan, phuongThucThanhToan) as JsonResult;
                // dynamic resultData = result.Data;
                // Assert
                Assert.IsNotNull(result, "No ActionResult returned from action method.");
            }


        }
    }
}

                     