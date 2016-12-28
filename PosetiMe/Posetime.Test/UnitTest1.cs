using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PosetiMe.Controllers;
using NUnit.Framework;
using NUnit;

namespace Posetime
{
    [TestFixture]
    public class UnitTest1
    {
        [TestMethod]
        public void CitiesIndex()
        {
            CitiesController city = new CitiesController();

            var result = city.Details(1);
            NUnit.Framework.Assert.AreEqual("Details", result);
        }
    }
}
