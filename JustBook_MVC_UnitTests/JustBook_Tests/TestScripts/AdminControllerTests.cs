using JustBook.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace JustBook_Tests.TestScripts
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
            var result = adminHomeController.OrderManagement();
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
    }
}
