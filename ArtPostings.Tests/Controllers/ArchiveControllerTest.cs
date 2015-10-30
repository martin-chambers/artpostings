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
    public class ArchiveControllerTest
    {
        PostingServiceMock mock;
        [TestInitialize]
        public void testInit()
        {
            mock = new PostingServiceMock();
        }
        [TestMethod]
        public void Archive_Index_SetResultNotNull_Success()
        {
            ArchiveController controller = new ArchiveController(mock);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Archive_Index_SetViewBag_Success()
        {
            ArchiveController controller = new ArchiveController(mock);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsTrue(result.ViewBag.Mode == "_itemDisplay");
        }
        [TestMethod]
        public void Archive_Index_SetResult_Success()
        {
            ArchiveController controller = new ArchiveController(mock);
            ViewResult result = controller.Index() as ViewResult;
            List<ItemPostingViewModel> postings = (List<ItemPostingViewModel>)result.Model;
            Assert.IsTrue(postings.Count == 3);
        }

        [TestMethod]
        public void Archive_Edit_SetViewName_Success()
        {
            ArchiveController controller = new ArchiveController(mock);
            int existentId = 5;
            PartialViewResult result = (PartialViewResult)controller.Edit(existentId);
            Assert.IsTrue(result.ViewName == "_itemDisplay");
        }
        [TestMethod]
        public void Archive_Edit_CallServiceEdit_Success()
        {
            ArchiveController controller = new ArchiveController(mock);
            int existentId = 2;
            PartialViewResult result = (PartialViewResult)controller.Edit(existentId);

            List<ItemPostingViewModel> postings = (List<ItemPostingViewModel>)result.Model;
            Assert.IsTrue(postings.Find(x => x.ItemPosting.Id == existentId).Editing);
        }
        [TestMethod]
        public void Archive_Edit_SetBlurb_Success()
        {
            ArchiveController controller = new ArchiveController(mock);
            int existentId = 2;
            PartialViewResult result = (PartialViewResult)controller.Edit(existentId);
            string blurb = result.ViewData["Blurb"].ToString();
            Assert.IsTrue(blurb == "This is Archive Text");
        }
        [TestMethod]
        public void Archive_Edit_SetMode_Success()
        {
            ArchiveController controller = new ArchiveController(mock);
            int existentId = 2;
            PartialViewResult result = (PartialViewResult)controller.Edit(existentId);
            string mode = result.ViewData["Mode"].ToString();
            Assert.IsTrue(mode == "_itemDisplay");
        }
    }
}
