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
    public class ContactControllerTest
    {
        PostingServiceMock mock;
        [TestInitialize]
        public void testInit()
        {
            mock = new PostingServiceMock();
        }
        [TestMethod]
        public void Contact_Index_SetResult_Success()
        {
            ContactController controller = new ContactController(mock);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        //             ViewBag.Mode = ;

        [TestMethod]
        public void Contact_Index_SetViewBag_Success()
        {
            ContactController controller = new ContactController(mock);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsTrue(result.ViewBag.Mode == "_noItemDisplay");
        }

    }
}
