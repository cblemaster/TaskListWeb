using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public class FolderSortFieldSqlDAO : IFolderSortFieldDAO
    {

        private string connectionString;

        // Single Parameter Constructor
        public FolderSortFieldSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public FolderSortField ConvertReaderToFolderSortField(SqlDataReader reader)
        {
            FolderSortField fsf = new FolderSortField
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["sort_field"])
            };
            return fsf;
        }

        public FolderSortField Get(int id)
        {
            FolderSortField fsf = new FolderSortField();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get the folder sort field
                SqlCommand cmd = new SqlCommand("sort_field_get_by_id", conn);
                //specify the command type as a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //create a new folder sort field object and return it
                    fsf = ConvertReaderToFolderSortField(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting folder sort field.");
                Console.WriteLine(e.Message);
                throw;
            }
            return fsf;
        }

        public FolderSortField Get(string name)
        {
            FolderSortField fsf = new FolderSortField();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get the folder sort field
                SqlCommand cmd = new SqlCommand("sort_field_get_by_name", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //create a new folder sort field object and return it
                    fsf = ConvertReaderToFolderSortField(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting folder sort field.");
                Console.WriteLine(e.Message);
                throw;
            }
            return fsf;
        }

        public IList<FolderSortField> List()
        {
            List<FolderSortField> list = new List<FolderSortField>();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get all of the folder sort fields
                SqlCommand cmd = new SqlCommand("sort_field_get_all", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //for every row, create a new folder sort field object and add it to the list
                    FolderSortField fsf = ConvertReaderToFolderSortField(reader);
                    list.Add(fsf);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting folder sort fields.");
                Console.WriteLine(e.Message);
                throw;
            }
            return list;
        }
    }
}
