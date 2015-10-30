using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtPostings.Controllers;
using ArtPostings.Models;
using ArtPostings.Tests.Models;

namespace ArtPostings.Tests.Controllers
{
    [TestClass]
    public class AboutControllerTest
    {
        PostingServiceMock mock;
        [TestInitialize]
        public void testInit()
        {
            mock = new PostingServiceMock();
        }
        [TestMethod]
        public void About_Index_SetResult_Success()
        {
            AboutController controller = new AboutController(mock);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
        
        [TestMethod]
        public void About_Index_SetViewBag_Success()
        {
            AboutController controller = new AboutController(mock);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsTrue(result.ViewBag.Mode == "_noItemDisplay");
        }
    }
}
