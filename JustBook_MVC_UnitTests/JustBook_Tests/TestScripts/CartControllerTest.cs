using JustBook.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Mvc;

namespace JustBook_Tests.TestScripts
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CartControllerTest
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetIndex_ShouldReturnListItemsInCart()
        {
            var controller = new CartController();
            var context = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            context.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            //context.Setup(p => p.HttpContext.Session["CartItem"]).Returns(null);
            //context.Setup(p => p.HttpContext.Session["CartCounter"]).Returns("1");
            //context.Setup(p => p.HttpContext.Session["TongSoLuongMua"]).Returns("1");
            //context.Setup(p => p.HttpContext.Session["MaKH"]).Returns(null);
            //context.Setup(p => p.HttpContext.Session["TongCong_temp"]).Returns(string.Format("{0:#,##0 VND}", 828550));
            //context.Setup(p => p.HttpContext.Session["TongCong"]).Returns(string.Format("{0:#,##0 VND}", 828550 - 828550 * 0.15));
            //context.Setup(p => p.HttpContext.Session["MaQT"]).Returns("4");
            controller.ControllerContext = context.Object;
            var result = controller.Index() as ViewResult;
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void PostIndex_ShouldReturnJsonResult()
        {
            var controller = new CartController();
            var context = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            context.Setup(p => p.HttpContext.Session).Returns(mockSession.Object);
            context.Setup(p => p.HttpContext.Session["CartItem"]).Returns(null);
            context.Setup(p => p.HttpContext.Session["CartCounter"]).Returns(null);
            context.Setup(p => p.HttpContext.Session["TongSoLuongMua"]).Returns("1");
            context.Setup(p => p.HttpContext.Session["MaKH"]).Returns(null);
            context.Setup(p => p.HttpContext.Session["TongCong_temp"]).Returns(string.Format("{0:#,##0 VND}", 828550));
            context.Setup(p => p.HttpContext.Session["TongCong"]).Returns(string.Format("{0:#,##0 VND}", 828550 - 828550 * 0.15));
            controller.ControllerContext = context.Object;
            var result = controller.Index("IT-04", 1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        [TestMethod]
        public void GetSoLuong_ShoudlReturnSoLuongSanPhamTheoI()
        {
            var controller = new CartController();
            var result = controller.getSoLuong("IT-04");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(JsonResult));
        }
        [TestMethod]
        public void SaveInfo_ShouldReturnJsonWithCustomerInfo()
        {
            var controller = new CartController();
            var context = new Mock<ControllerContext>();
            var mockSession = new Mock<HttpSessionStateBase>();
            context.Setup(p => p.HttpContext.Session["TenKH_GiaoHang"]).Returns("");
            context.Setup(p => p.HttpContext.Session["Phone_GiaoHang"]).Returns("");
            context.Setup(p => p.HttpContext.Session["DiaChi_GiaoHang"]).Returns("");
            controller.ControllerContext = context.Object;
            var result = controller.SaveInfo("Nguyen Dinh Bao", "0938833412", "69 ngo tat to");
            var resultData = result.Data;
            var expectedData = new { Success = true, TenKH = "Nguyen Dinh Bao", Phone = "0938833412", DiaChi = "69 ngo tat to" };
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedData.ToString(), resultData.ToString());

        }
    }
}
