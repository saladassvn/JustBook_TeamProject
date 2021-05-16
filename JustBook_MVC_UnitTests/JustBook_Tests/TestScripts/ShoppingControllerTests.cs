using System;
using JustBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JustBook.Controllers;
using JustBook.Models;
using System.Web.Mvc;
using Moq;
using JustBook.ViewModel;

namespace JustBook_Tests.TestScripts
{
    [TestClass]
    public class ShoppingControllerTests
    {
        [TestMethod]
        public void Index_WithCorrectProductID_ShouldReturnSuscessful()
        {
            // Arange
            ShoppingController shoppingController = new ShoppingController();
            int maLoaiSP = 1;
            // Action
            var result = shoppingController.Index(maLoaiSP) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
