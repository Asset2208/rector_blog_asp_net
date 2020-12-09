using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rector_blog.Controllers;


namespace rector_blog.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void AboutViewResultNotNull()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AboutViewEqualAboutCshtml()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            Assert.AreEqual("About", result.ViewName);
        }

        [TestMethod]
        public void AboutStringInViewbag()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void IndexViewResultNotNull()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }


        [TestMethod]
        public void ContactViewResultNotNull()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ContactViewEqualContactCshtml()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Contact() as ViewResult;

            Assert.AreEqual("Contact", result.ViewName);
        }

        [TestMethod]
        public void ContactStringInViewbag()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Contact() as ViewResult;

            Assert.AreEqual("Your contact page.", result.ViewBag.Message);
        }
    }
}
