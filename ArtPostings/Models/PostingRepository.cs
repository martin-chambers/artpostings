using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ArtPostings.Models
{
    public class PostingRepository : IPostingRepository
    {

        
        private string webSafePictureFolder = ConfigurationManager.AppSettings["pictureLocation"];
        //private string pictureFolder = HttpContext.Current.Server.MapPath("~/Content/Art");        
        string connectionString = ConfigurationManager.ConnectionStrings["ArtPostings"].ConnectionString;
        bool IPostingRepository.Create(ItemPosting posting)
        {
            throw new NotImplementedException();
        }
        IEnumerable<ItemPosting> IPostingRepository.ShopPostings()
        {
            string commandText = "SELECT [Id],[Order],[Filename],[Title],[Shortname],[Header], " +
                "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems] WHERE Archive_Flag = 0";
            return getPostings(commandText);
        }
        IEnumerable<ItemPosting> IPostingRepository.ArchivePostings()
        {
            string commandText = "SELECT [Id],[Order],[Filename],[Title],[Shortname],[Header], " +
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
                                reader["Order"].ToString(),
                                Path.Combine(webSafePictureFolder, reader["Filename"].ToString()),
                                reader["Filename"].ToString(),
                                reader["Title"].ToString(),
                                reader["Shortname"].ToString(),
                                reader["Header"].ToString(),
                                reader["Description"].ToString(),
                                reader["Size"].ToString(),
                                reader["Price"].ToString(),
                                reader["Archive_Flag"].ToString()
                                )
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading from ArtPostitems data source", ex);
            }
            return postings;
        }
 
        ItemPosting IPostingRepository.GetPosting(int id)
        {
            return this.selectPosting(id, "", "");
        }
        ItemPosting selectPosting(int id, string selectString, string selectField)
        {
            try
            {
                ItemPosting posting = new ItemPosting();
                string commandText =

                    (!string.IsNullOrEmpty(selectString)) 

                    ? "SELECT [Id],[Filename],[Title],[Shortname],[Header], " +
                    "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems]" +
                    " WHERE (Id = @Id) AND (" + selectField + " = '' OR " + selectField + " = @selectValue"

                    : "SELECT[Id],[Filename],[Title],[Shortname],[Header], " +
                    "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems]" +
                    " WHERE (Id = @Id)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add(new SqlParameter("@Id", id));
                    command.Parameters.Add(new SqlParameter("@selectValue", selectString));
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        posting = new ItemPosting(
                            reader["Id"].ToString(),
                            reader["Order"].ToString(),
                            reader["Filename"].ToString(),
                            Path.Combine(webSafePictureFolder, reader["Filename"].ToString()),
                            reader["Title"].ToString(),
                            reader["Shortname"].ToString(),
                            reader["Header"].ToString(),
                            reader["Description"].ToString(),
                            reader["Size"].ToString(),
                            reader["Price"].ToString(),
                            reader["Archive_Flag"].ToString()
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
        void IPostingRepository.Update(ItemPosting itemposting, bool archived)
        {

            //try
            //{
            string commandText = "Update [dbo].[ArtPostingItems] " +
                "SET [Order] = '" + itemposting.Order + "'," +
                "[Filename] = '" + itemposting.FileName + "'," +
                "[Title] = '" + itemposting.Title + "'," +
                "[Shortname] = '" + itemposting.ShortName + "'," +
                "[Header] = '" + itemposting.Header + "'," +
                "[Description] = '" + itemposting.Description + "'," +
                "[Size] = '" + itemposting.Size + "'," +
                "[Price] = '" + itemposting.Price + "'," +
                "[Archive_flag] = '" + archived.ToString() +
                "' WHERE id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                connection.Open();
                adapter.UpdateCommand = connection.CreateCommand();
                adapter.UpdateCommand.CommandText = commandText;
                SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);
                idParam.Value = itemposting.Id;
                adapter.UpdateCommand.Parameters.Add(idParam);
                adapter.UpdateCommand.ExecuteNonQuery();
            }
        //}
        // no posting object?
        //catch (NullReferenceException nullex)
        //{
        //    throw new Exception("Could not retrieve art posting to update", nullex);
        //}
        //catch (SqlException sqlex)
        //{
        //    Console.WriteLine("Could not update database for art posting item " + posting.Title + ": " + sqlex.Message);
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception("Could not update database for art posting item " + posting.Title, ex);
        //}

    }
    }    
}