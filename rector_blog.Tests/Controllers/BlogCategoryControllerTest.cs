using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rector_blog.Controllers;
using rector_blog.Models;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace rector_blog.Tests.Controllers
{
    [TestClass]
    public class BlogCategoryControllerTest
    {
        [TestMethod]
        public void IndexViewModelNotNull()
        {
            BlogCategoryController controller = new BlogCategoryController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void CreateViewResultNotNull()
        {
            BlogCategoryController controller = new BlogCategoryController();

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        /*[TestMethod]
        public void CreateAction_RedirectToIndexView()
        {
            var mock = new Mock<DbSet<BlogCategoryModels>>();
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.BlogCategoryModel).Returns(mock.Object);

            var controller = new BlogCategoryController(mockContext.Object);
            var blogCategory = new BlogCategoryModels();
            controller.Create(blogCategory);

            RedirectToRouteResult result = controller.Create(blogCategory) as RedirectToRouteResult;

            mock.Verify(a => a.Add(It.IsAny<BlogCategoryModels>()));
       


        }*/
    }
}
