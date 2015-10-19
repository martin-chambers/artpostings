using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtPostings.Controllers;
using ArtPostings.Models;
using ArtPostings.Tests.Models;

namespace ArtPostings.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        PostingServiceMock mock;
        [TestInitialize]
        public void testInit()
        {
            mock = new PostingServiceMock();
        }
        [TestMethod]
        public void Index_SetResult_Success()
        {
            HomeController controller = new HomeController(mock);
            ViewResult result = controller.Index() as ViewResult;
            List<ItemPostingViewModel> postings = (List<ItemPostingViewModel>)result.Model;
            Assert.IsTrue(postings.Count == 3);
        }

        [TestMethod]
        public void Edit_SetViewName_Success()
        {
            HomeController controller = new HomeController(mock);
            int existentId = 5;
            PartialViewResult result = (PartialViewResult)controller.Edit(existentId);
            Assert.IsTrue(result.ViewName == "_itemDisplay");
        }
        [TestMethod]
        public void Edit_CallServiceEdit_Success()
        {
            HomeController controller = new HomeController(mock);
            int existentId = 5;
            PartialViewResult result = (PartialViewResult)controller.Edit(existentId);

            List<ItemPostingViewModel> postings = (List<ItemPostingViewModel>)result.Model;
            Assert.IsTrue(postings.Find(x => x.Posting.Id == existentId).Editing);
        }
        [TestMethod]
        public void Edit_SetBlurb_Success()
        {
            HomeController controller = new HomeController(mock);
            int existentId = 5;
            PartialViewResult result = (PartialViewResult)controller.Edit(existentId);
            string blurb = result.ViewData["Blurb"].ToString();
            Assert.IsTrue(blurb == "This is Shop Text");
        }
        [TestMethod]
        public void Edit_SetMode_Success()
        {
            HomeController controller = new HomeController(mock);
            int existentId = 5;
            PartialViewResult result = (PartialViewResult)controller.Edit(existentId);
            string mode = result.ViewData["Mode"].ToString();
            Assert.IsTrue(mode == "_itemDisplay");
        }
    }
}
