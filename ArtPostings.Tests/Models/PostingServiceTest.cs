using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArtPostings.Controllers;
using ArtPostings.Models;
using System.IO;

namespace ArtPostings.Tests.Models
{
    [TestClass]
    public class PostingServiceTest
    {

        private string tempPath;
        [TestInitialize]
        public void testInit()
        {
            tempPath = Path.GetTempPath();
            Directory.CreateDirectory(tempPath);
            // test file 1
            string f1 = File.Create(Path.Combine(tempPath, Path.GetTempFileName())).Name;
            // test file 2
            string f2 = File.Create(Path.Combine(tempPath, Path.GetTempFileName())).Name;
            

        }

        [TestMethod]
        ///Testing concrete class singleton retrieval
        public void PostingService_Instance_AreSame_Success()
        {
            //PostingService ps1 = PostingService.Instance;
            //PostingService ps2 = PostingService.Instance;
            //Assert.AreSame(ps1, ps2);
        }

        
        // will require injection of repository to ProductService:

        //[TestMethod]
        /////Testing concrete class singleton retrieval
        //public void PostingService_PictureFileRecordList_Success()
        //{
        //    PostingService ps = PostingService.Instance;
        //    List<PictureFileRecord> list = ps.PictureFileRecordList(tempPath);


        //}

    }
}
