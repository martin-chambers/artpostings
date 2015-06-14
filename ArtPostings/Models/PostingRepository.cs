using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ArtPostings.Models
{
    public class PostingRepository : IPostingRepository
    {
        // this won't stay here
        string pictureFolder = "../../Content/art/";
        string connectionString = "Data Source=HALL-DESKTOP;Initial Catalog=ArtPostings;Persist Security Info=True;User ID=artPostingsUser;Password='aPU_16%$'";
        bool IPostingRepository.Create(ItemPosting posting)
        {
            throw new NotImplementedException();
        }

        IEnumerable<ItemPosting> IPostingRepository.ShopPostings()
        {
            List<ItemPosting> postings = new List<ItemPosting>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string commandText = "SELECT [Id],[Filename],[Title],[Shortname],[Header], " +
                    "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems] WHERE Archive_Flag = 0";
                SqlCommand command = new SqlCommand(commandText, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    postings.Add(
                        new ItemPosting(
                            pictureFolder + reader["Filename"].ToString(),
                            reader["Title"].ToString(),
                            reader["Shortname"].ToString(),
                            reader["Header"].ToString(),
                            reader["Description"].ToString(),
                            reader["Size"].ToString(),
                            reader["Price"].ToString()
                            )
                    );
                }
            }
            return postings;
        }
        IEnumerable<ItemPosting> IPostingRepository.ArchivePostings()
        {
            List<ItemPosting> postings = new List<ItemPosting>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string commandText = "SELECT [Id],[Filename],[Title],[Shortname],[Header], " + 
                    "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems] WHERE Archive_Flag = 1";
                SqlCommand command = new SqlCommand(commandText, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    postings.Add(
                        new ItemPosting(
                            pictureFolder + reader["Filename"].ToString(), 
                            reader["Title"].ToString(),
                            reader["Shortname"].ToString(), 
                            reader["Header"].ToString(), 
                            reader["Description"].ToString(), 
                            reader["Size"].ToString(), 
                            reader["Price"].ToString()
                            )
                    );
                }

            }           
            return postings;
        }
    }
}