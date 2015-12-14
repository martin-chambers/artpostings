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

        ChangeResult IPostingRepository.Create(ItemPosting itemposting, bool archive)
        {
            string commandText =
                "INSERT INTO [dbo].[ArtPostingItems]" +
                "([Filename]" +
                ",[Title]" +
                ",[Shortname]" +
                ",[Header]" +
                ",[Description]" +
                ",[Size]" +
                ",[Price]" +
                ",[Archive_flag]" +
                ",[Order]) " +
                "VALUES " +
                "( @filename" +
                ", @title" +
                ", @shortName" +
                ", @header" +
                ", @desc" +
                ", @size" +
                ", @price" +
                ", @archive" +
                ", @order)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.UpdateCommand = new SqlCommand(commandText, connection, transaction);
                        adapter = addPostingParamsToAdapter(adapter, itemposting, archive);
                        updateOrderValues(connection, transaction, archive, true);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new ChangeResult(false, "Error inserting posting with filename: " + itemposting.FileName + ": " + ex.Message);
                    }
                }
            }
            return new ChangeResult(true, "Inserted posting with filename: " + itemposting.FileName);
        }
        /// <summary>
        /// Updates all Order values upwards or downwards for a specified list (archive or !archive) 
        /// where the order value is greater than that specified. This is intended to be called when 
        /// an item is being inserted or deleted. It is assumed that this method will be called from 
        /// a context in which there is an open connection, and an open transaction.
        /// </summary>
        /// <param name="excludeId"></param>
        private void updateOrderValues(SqlConnection connection, SqlTransaction transaction, bool archive, bool increment, int order = 0)
        {
            string inc_symbol = (increment) ? "+" : "-";
            string arch_val = (archive) ? "'true'" : "'false'";
            string commandText = "Update [dbo].[ArtPostingItems] SET [Order] = [Order] " + inc_symbol +
                " 1 WHERE Archive_flag = " + arch_val + " AND [Order] >= " + order.ToString();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand(commandText, connection, transaction);
            adapter.UpdateCommand.ExecuteNonQuery();
        }
        public IEnumerable<ItemPosting> ShopPostings()
        {
            string commandText = "SELECT [Id],[Order],[Filename],[Title],[Shortname],[Header], " +
                "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems] WHERE Archive_Flag = 0 ORDER BY [Order]";
            return getPostings(commandText);
        }
        public IEnumerable<ItemPosting> ArchivePostings()
        {
            string commandText = "SELECT [Id],[Order],[Filename],[Title],[Shortname],[Header], " +
                "[Description],[Size],[Price],[Archive_Flag] FROM [dbo].[ArtPostingItems] WHERE Archive_Flag = 1 ORDER BY [Order]";
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

        ItemPosting IPostingRepository.GetPosting(Predicate<ItemPosting> predicate, bool archive)
        {
            List<ItemPosting> postings = new List<ItemPosting>();
            if (archive)
            {
                postings.AddRange(ArchivePostings().ToList());
            }
            else
            {
                postings.AddRange(ShopPostings().ToList());
            }

            ItemPosting posting = postings.Find(predicate);
            return posting;
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
        private SqlDataAdapter addPostingParamsToAdapter(SqlDataAdapter _adapter, ItemPosting itemposting, bool archived)
        {
            SqlDataAdapter adapter = _adapter;

            SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);
            idParam.Value = itemposting.Id;
            adapter.UpdateCommand.Parameters.Add(idParam);

            SqlParameter orderParam = new SqlParameter("@order", SqlDbType.Int);
            orderParam.Value = itemposting.Order;
            adapter.UpdateCommand.Parameters.Add(orderParam);

            SqlParameter filenameParam = new SqlParameter("@filename", SqlDbType.NVarChar);
            filenameParam.Value = itemposting.FileName;
            adapter.UpdateCommand.Parameters.Add(filenameParam);

            SqlParameter titleParam = new SqlParameter("@title", SqlDbType.NVarChar);
            titleParam.Value = itemposting.Title;
            adapter.UpdateCommand.Parameters.Add(titleParam);

            SqlParameter shortnameParam = new SqlParameter("@shortname", SqlDbType.NVarChar);
            shortnameParam.Value = itemposting.ShortName;
            adapter.UpdateCommand.Parameters.Add(shortnameParam);

            SqlParameter headerParam = new SqlParameter("@header", SqlDbType.NVarChar);
            headerParam.Value = itemposting.Header;
            adapter.UpdateCommand.Parameters.Add(headerParam);

            SqlParameter descParam = new SqlParameter("@desc", SqlDbType.NVarChar);
            descParam.Value = itemposting.Description;
            adapter.UpdateCommand.Parameters.Add(descParam);

            SqlParameter sizeParam = new SqlParameter("@size", SqlDbType.NVarChar);
            sizeParam.Value = itemposting.Size;
            adapter.UpdateCommand.Parameters.Add(sizeParam);

            SqlParameter priceParam = new SqlParameter("@price", SqlDbType.NVarChar);
            priceParam.Value = itemposting.Price;
            adapter.UpdateCommand.Parameters.Add(priceParam);

            SqlParameter archiveParam = new SqlParameter("@archive", SqlDbType.NVarChar);
            archiveParam.Value = archived.ToString();
            adapter.UpdateCommand.Parameters.Add(archiveParam);

            return adapter;
        }
        ChangeResult IPostingRepository.Update(ItemPosting itemposting, bool archived)
        {
            try
            {
                string commandText = "Update [dbo].[ArtPostingItems] " +
                    "SET [Order] = @order," +
                "[Filename] = @filename, " +
                "[Title] = @title, " +
                "[Shortname] = @shortname, " +
                "[Header] = @header, " +
                "[Description] = @desc, " +
                "[Size] = @size, " +
                "[Price] = @price, " +
                "[Archive_flag] = @archive" +
                " WHERE id = @id";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    connection.Open();
                    adapter.UpdateCommand = connection.CreateCommand();
                    adapter.UpdateCommand.CommandText = commandText;
                    adapter = addPostingParamsToAdapter(adapter, itemposting, archived);
                    adapter.UpdateCommand.ExecuteNonQuery();
                }
                return new ChangeResult(true, "Posting: " + itemposting.Id.ToString() + " was updated");
            }
            catch (Exception ex)
            {
                return new ChangeResult(false, "Posting: " + itemposting.Id.ToString() + " could not be updated: " + ex.Message);
            }
        }
        ChangeResult IPostingRepository.Delete(ItemPosting itemposting)
        {
            bool archive = itemposting.Archive_Flag == true;
            int deleted = 0;
            string commandText = "DELETE FROM [dbo].[ArtPostingItems] WHERE [Filename] = @filename";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.DeleteCommand = new SqlCommand(commandText, connection, transaction);
                        SqlParameter filenameParam = new SqlParameter("@filename", SqlDbType.NVarChar);
                        filenameParam.Value = itemposting.FileName.Normalise();
                        adapter.DeleteCommand.Parameters.Add(filenameParam);
                        updateOrderValues(connection, transaction, archive, false, itemposting.Order);
                        deleted = adapter.DeleteCommand.ExecuteNonQuery();
                        if (deleted > 0)
                        {
                            transaction.Commit();
                            return new ChangeResult(true, "Posting: " + itemposting.FileName + " was deleted from the database");
                        }
                        else
                        {
                            transaction.Rollback();
                            return new ChangeResult(false, "Posting: " + itemposting.FileName + " could not be deleted from the database");
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new ChangeResult(false, "Posting: " + itemposting.FileName + " could not be deleted from the database - error: " + ex.Message);
                    }
                }
            }

        }
        /// <summary>
        /// Exchange the order values of posting1 and posting2
        /// </summary>
        /// <param name="posting1"></param>
        /// <param name="posting2"></param>
        /// <param name="archived"></param>
        /// <returns></returns>
        ChangeResult IPostingRepository.ExchangeOrders(ItemPosting posting1, ItemPosting posting2, bool archived)
        {
            int order1 = posting1.Order;
            int order2 = posting2.Order;
            ChangeResult result = new ChangeResult();
            string commandText = "Update [dbo].[ArtPostingItems] " +
                "SET [Order] = @neworder " +
                "WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    int updated = 0;
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        // update #1: set 1 to 2
                        adapter.UpdateCommand = new SqlCommand(commandText, connection, transaction);
                        SqlParameter newOrderParam = new SqlParameter("@neworder", SqlDbType.Int);
                        newOrderParam.Value = order2;
                        SqlParameter idParam = new SqlParameter("@id", SqlDbType.Int);
                        idParam.Value = posting1.Id;
                        adapter.UpdateCommand.Parameters.Add(newOrderParam);
                        adapter.UpdateCommand.Parameters.Add(idParam);
                        updated = adapter.UpdateCommand.ExecuteNonQuery();

                        // update #2: set 2 to 1
                        if (updated > 0)
                        {
                            updated = 0;
                            newOrderParam.Value = order1;
                            idParam.Value = posting2.Id;
                            updated = adapter.UpdateCommand.ExecuteNonQuery();
                        }

                        // commit
                        if (updated > 0)
                        {
                            transaction.Commit();
                        }
                        else
                        {
                            throw new Exception("There was a problem changing the orders of " + posting1.FileName + " and " + posting2.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new ChangeResult(false, ex.Message);
                    }
                }
                result = new ChangeResult(true, "Promoted: " + posting1.FileName.Normalise());
                return result;
            }
        }
    }
}