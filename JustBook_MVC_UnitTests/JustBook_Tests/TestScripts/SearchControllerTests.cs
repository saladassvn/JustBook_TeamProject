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
    public class SearchControllerTests
    {
        [TestMethod]
        public void Index_WithCorrectInputValue_ShouldSearchSucessful()
        {
            // Arange
            SearchController searchController = new SearchController();
            string searchString = "Coding";
            // Action
            var result = searchController.Index(searchString) as ViewResult;
            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
