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
    public class LoginControllerTest
    {
        //Cach dat ten Test Method:
        // TenHam_DieuKien_KetQua
        [TestMethod]
        public void Verify_WithCorrectInputValue_ShouldLoginSucessful()
        {
            // Arange
            LoginController loginController = new LoginController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                Email = "dat123@gmail.com",
                MatKhau = "123456",
                MaKH = 15,
                TenKH = "Bui Quoc Dat",
                DiaChi = "123 hung vuong",
                Phone = 59113
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            loginController.ControllerContext = mock.Object;

            // Action
            var result = loginController.Verify(model) as RedirectToRouteResult;
            // Assert
            //Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Verify_WithWrongPasswordInputValue_ShouldLoginUnsuccessful()
        {
            // Arange
            LoginController loginController = new LoginController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                Email = "dat123@gmail.com",
                //Wrong password:
                MatKhau = "123456789"

            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            loginController.ControllerContext = mock.Object;

            // Action
            ViewResult result = loginController.Verify(model) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Verify_WithWrongEmailInputValue_ShouldLoginUnsuccessful()
        {
            // Arange
            LoginController loginController = new LoginController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                //Wrong Email:
                Email = "dat123123123@gmail.com",
                MatKhau = "123456"

            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            loginController.ControllerContext = mock.Object;

            // Action
            ViewResult result = loginController.Verify(model) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Verify_WithWrongEmailInputAndPasswordInputValue_ShouldLoginUnsuccessful()
        {
            // Arange
            LoginController loginController = new LoginController();

            TaiKhoanKH model = new TaiKhoanKH()
            {
                //Wrong Email:
                Email = "dat123123123@gmail.com",
                MatKhau = "123456789"

            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            loginController.ControllerContext = mock.Object;

            // Action
            ViewResult result = loginController.Verify(model) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AdminVerify_WithCorrectInputValue_ShouldLoginSucessful()
        {
            // Arange
            LoginController loginController = new LoginController();

            TaiKhoanQT model = new TaiKhoanQT()
            {
                Email = "admin@gmail.com",
                MatKhau = "123456",
            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            loginController.ControllerContext = mock.Object;

            // Action
            var result = loginController.AdminVerify(model) as RedirectToRouteResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void AdminVerify_WithWrongPasswordInputValue_ShouldLoginUnsuccessful()
        {
            // Arange
            LoginController loginController = new LoginController();

            TaiKhoanQT model = new TaiKhoanQT()
            {
                Email = "admin@gmail.com",
                //Wrong password:
                MatKhau = "123456789"

            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            loginController.ControllerContext = mock.Object;

            // Action
            ViewResult result = loginController.AdminVerify(model) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AdminVerify_WithWrongEmailInputValue_ShouldLoginUnsuccessful()
        {
            // Arange
            LoginController loginController = new LoginController();

            TaiKhoanQT model = new TaiKhoanQT()
            {
                //Wrong email:
                Email = "adminadmin@gmail.com",
                MatKhau = "123456"

            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            loginController.ControllerContext = mock.Object;

            // Action
            ViewResult result = loginController.AdminVerify(model) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void AdminVerify_WithWrongEmailAndPasswordInputValue_ShouldLoginUnsuccessful()
        {
            // Arange
            LoginController loginController = new LoginController();

            TaiKhoanQT model = new TaiKhoanQT()
            {
                //Wrong email:
                Email = "adminadmin@gmail.com",
                //Wrong password:
                MatKhau = "123456789"

            };
            var mock = new Mock<ControllerContext>();
            var mockSession = new Mock<System.Web.HttpSessionStateBase>();
            mock.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            loginController.ControllerContext = mock.Object;

            // Action
            ViewResult result = loginController.AdminVerify(model) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
