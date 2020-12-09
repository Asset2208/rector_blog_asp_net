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
    public class QuestionBlogPostControllerTest
    {
        [TestMethod]
        public void IndexViewModelNotNull()
        {
            QuestionBlogPostController controller = new QuestionBlogPostController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void CreateViewResultNotNull()
        {
            QuestionBlogPostController controller = new QuestionBlogPostController();

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
