using Microsoft.VisualStudio.TestTools.UnitTesting;
using PosetiMe.Controllers;
using PosetiMe.Models;
using System.Web.Mvc;
using NUnit;

namespace PosetiMeTests
{
    [TestClass]
    public class LocalsControllerTest
    {
        public DBPosetiMeEntities db = new DBPosetiMeEntities();
        [TestMethod]
        public void TestLocalsControllerCreateView()
        {
            var controller = new LocalsController();
            ActionResult result = controller.Create() as ViewResult;
            //Assert.AreEqual("Create", result.ViewName);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void TestLocalsControllerCreateName()
        {
            var controller = new LocalsController();
            ActionResult result = controller.Create() as ViewResultBase;
            string r = (result as ViewResult).ViewName;
            Assert.AreEqual("Create", r);
        }
    }
}
