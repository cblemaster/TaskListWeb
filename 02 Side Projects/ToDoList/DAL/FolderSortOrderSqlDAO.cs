using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public class FolderSortOrderSqlDAO : IFolderSortOrderDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public FolderSortOrderSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public FolderSortOrder ConvertReaderToFolderSortOrder(SqlDataReader reader)
        {
            FolderSortOrder fso = new FolderSortOrder
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["sort_order"])
            };
            return fso;
        }

        public FolderSortOrder Get(int id)
        {
            FolderSortOrder fso = new FolderSortOrder();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get the folder sort order
                SqlCommand cmd = new SqlCommand("sort_order_get_by_id", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //create a new folder sort order object and return it
                    fso = ConvertReaderToFolderSortOrder(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting folder sort order.");
                Console.WriteLine(e.Message);
                throw;
            }
            return fso;
        }

        public FolderSortOrder Get(string name)
        {
            FolderSortOrder fso = new FolderSortOrder();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get the folder sort order
                SqlCommand cmd = new SqlCommand("sort_order_get_by_name", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //create a new folder sort order object and return it
                    fso = ConvertReaderToFolderSortOrder(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting folder sort order.");
                Console.WriteLine(e.Message);
                throw;
            }
            return fso;
        }

        public IList<FolderSortOrder> List()
        {
            List<FolderSortOrder> list = new List<FolderSortOrder>();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get all of the folder sort orders
                SqlCommand cmd = new SqlCommand("sort_order_get_all", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //for every row, create a new folder sort order object and add it to the list
                    FolderSortOrder fso = ConvertReaderToFolderSortOrder(reader);
                    list.Add(fso);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting folder sort orders.");
                Console.WriteLine(e.Message);
                throw;
            }
            return list;
        }
    }
}
