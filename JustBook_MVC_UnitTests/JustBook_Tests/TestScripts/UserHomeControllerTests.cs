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

namespace JustBook_Tests.TestScripts.UserHomeControllerTests
{
    [TestClass]
    public class UserHomeControllerTest
    {
        // Index
        [TestMethod]
        public void Index_UserAccount_ShouldReturnUserInfo()
        {
            var userHomeController = new UserHomeController();
            var controllerContext = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            controllerContext.Setup(p => p.HttpContext.Session["MaKH"]).Returns("20");
            userHomeController.ControllerContext = controllerContext.Object;
            var result = userHomeController.Index() as ViewResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        // UserEditInformation
        [TestMethod]
        public void UserEditInformation_WithCorrectInputValue_SuccessfulFix()
        {
            // Arange
            UserHomeController userHomeController = new UserHomeController();
            DateTime dt2 = new DateTime(2001, 05, 29);

            TaiKhoanKH model = new TaiKhoanKH()
            {
                MaKH = 21,
                Email = "hoa123@gmail.com",
                MatKhau = "123456",
                TenKH = "Lý Quốc Hòa",
                DiaChi = "Long An",
                Phone = 53535353,
                GioiTinh = "Nam",
                NgaySinh = dt2
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userHomeController.ControllerContext = mock.Object;

            // Action
            var result = userHomeController.UserEditInformation(model) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void UserEditInformation_EnterEmail_SuccessfulFix()
        {
            // Arange
            UserHomeController userHomeController = new UserHomeController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                Email = "hoa123@gmail.com"
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userHomeController.ControllerContext = mock.Object;

            // Action
            var result = userHomeController.UserEditInformation(model) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void UserEditInformation_EnterName_SuccessfulFix()
        {
            // Arange
            UserHomeController userHomeController = new UserHomeController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                TenKH = "Lý Quốc Hòa"
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userHomeController.ControllerContext = mock.Object;

            // Action
            var result = userHomeController.UserEditInformation(model) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void UserEditInformation_EnterAddress_SuccessfulFix()
        {
            // Arange
            UserHomeController userHomeController = new UserHomeController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                DiaChi = "Long An"
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userHomeController.ControllerContext = mock.Object;

            // Action
            var result = userHomeController.UserEditInformation(model) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void UserEditInformation_EnterPhone_SuccessfulFix()
        {
            // Arange
            UserHomeController userHomeController = new UserHomeController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                Phone = 53535353
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userHomeController.ControllerContext = mock.Object;

            // Action
            var result = userHomeController.UserEditInformation(model) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void UserEditInformation_ChooseGender_SuccessfulFix()
        {
            // Arange
            UserHomeController userHomeController = new UserHomeController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                GioiTinh = "Nam"
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userHomeController.ControllerContext = mock.Object;

            // Action
            var result = userHomeController.UserEditInformation(model) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        [TestMethod]
        public void UserEditInformation_ChooseDateofbirth_SuccessfulFix()
        {
            // Arange
            UserHomeController userHomeController = new UserHomeController();
            DateTime dt2 = new DateTime(2001, 05, 29);

            TaiKhoanKH model = new TaiKhoanKH()
            {
                NgaySinh = dt2
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            userHomeController.ControllerContext = mock.Object;

            // Action
            var result = userHomeController.UserEditInformation(model) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
        }

        // OrderHistory
        [TestMethod]
        public void OrderHistory_OrderSuccess_OrderCreationSuccessful()
        {
            // Arange
            var userHomeController = new UserHomeController();
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session["MaKH"]).Returns("20");
            userHomeController.ControllerContext = mock.Object;

            // Action
            var result = userHomeController.OrderHistory() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        // OrderUserDetail
        [TestMethod]
        public void OrderUserDetail_Ordered_DisplayProductDetailPage()
        {
            // Arange
            var userHomeController = new UserHomeController();

            // Action
            var result = userHomeController.OrderUserDetail(50) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        // TrackingState
        [TestMethod]
        public void TrackingState_Ordered_ShowProductStatusPage()
        {
            // Arange
            var userHomeController = new UserHomeController();

            // Action
            var result = userHomeController.TrackingState(50) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

    }
}
