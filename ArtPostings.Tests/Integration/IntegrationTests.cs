using System;
using System.Configuration;
using System.IO;
using ArtPostings.Controllers;
using ArtPostings.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;


namespace ArtPostings.Tests.Integration
{
    [TestClass]
    public class IntegrationTests
    {
        [TestInitialize]
        public void testInit()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ArtPostingsTest"].ConnectionString;

            FileInfo fileForSaleItems = new FileInfo(@"../database/ArtPostingItems.table.insert.for_sale");
            string scriptForSaleItems = fileForSaleItems.OpenText().ReadToEnd();
            scriptForSaleItems = scriptForSaleItems.Replace("GO", "");

            FileInfo fileArchiveItems = new FileInfo(@"../database/ArtPostingItems.table.insert.archive");
            string scriptArchiveItems = fileArchiveItems.OpenText().ReadToEnd();
            scriptArchiveItems = scriptArchiveItems.Replace("GO", "");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = scriptForSaleItems;
                command.ExecuteNonQuery();
                command.CommandText = scriptArchiveItems;
                command.ExecuteNonQuery();
            }
        }

        [TestMethod]
        public void PostingRepository_ExchangeOrders_SetOrder_Success()
        {

        }




    }
}
