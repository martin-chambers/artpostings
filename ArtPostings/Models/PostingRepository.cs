using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ArtPostings.Models
{
    public class PostingRepository : IPostingRepository
    {
        string pictureFolder = ConfigurationManager.AppSettings["pictureLocation"];
        string connectionString = ConfigurationManager.ConnectionStrings["ArtPostings"].ConnectionString;
        bool IPostingRepository.Create(ItemPosting posting)
        {
            throw new NotImplementedException();
        }
        IEnumerable<ItemPosting> IPostingRepository.ShopPostings()
        {
            string commandText = "SELECT [Id],[Filename],[Title],[Shortname],[Header], " +
                "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems] WHERE Archive_Flag = 0";
            return getPostings(commandText);
        }
        IEnumerable<ItemPosting> IPostingRepository.ArchivePostings()
        {
            string commandText = "SELECT [Id],[Filename],[Title],[Shortname],[Header], " + 
                "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems] WHERE Archive_Flag = 1";
            return getPostings(commandText);
        }
        private IEnumerable<ItemPosting> getPostings(string commandText)
        {
            List<ItemPosting> postings = new List<ItemPosting>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(commandText, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        postings.Add(
                            new ItemPosting(
                                reader["Id"].ToString(),
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
            }
            catch (Exception ex) { }
            return postings;
        }
        ItemPosting IPostingRepository.GetPosting(int id)
        {
            try
            {
                ItemPosting posting = new ItemPosting();
                string commandText = "SELECT [Id],[Filename],[Title],[Shortname],[Header], " +
                    "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems] WHERE Id = @Id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        posting = new ItemPosting(
                            reader["Id"].ToString(),
                            pictureFolder + reader["Filename"].ToString(),
                            reader["Title"].ToString(),
                            reader["Shortname"].ToString(),
                            reader["Header"].ToString(),
                            reader["Description"].ToString(),
                            reader["Size"].ToString(),
                            reader["Price"].ToString()
                            );
                    }
                    return posting;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("Error retrieving item posting with id: " + id.ToString(), ex));
            }
        }
    }    
}