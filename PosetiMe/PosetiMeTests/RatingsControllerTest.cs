using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PosetiMe.Controllers;
using System.Web.Mvc;

namespace PosetiMeTests
{
    [TestClass]
    public class RatingsControllerTest
    {
        [TestClass]
        public class RatingControllerCreateTest
        {
            [TestMethod]
            public void TestLocalsControllerCreateView()
            {
                var controller = new RatingsController();
                ActionResult result = controller.Create() as ViewResult;
                //Assert.AreEqual("Create", result.ViewName);
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
        }
        [TestMethod]
        public void TestRatingsControllerCreateName()
        {
            var controller = new RatingsController();
            ActionResult result = controller.Create() as ViewResultBase;
            string r = (result as ViewResult).ViewName;
            Assert.AreEqual("Create", r);
        }
    }
}
