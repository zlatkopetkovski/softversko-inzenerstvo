using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using PosetiMe.Controllers;
using System.Web.Mvc;

namespace PosetiMeTests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestHomeControllerIndex()
        {
            HomeController controller = new HomeController();
            ActionResult result = controller.Index() as ViewResult;

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
        }
    }
}
