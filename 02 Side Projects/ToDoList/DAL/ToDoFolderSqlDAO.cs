using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public class ToDoFolderSqlDAO : IToDoFolderDAO
    {

        private string connectionString;

        // Single Parameter Constructor
        public ToDoFolderSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public ToDoFolder ConvertReaderToToDoFolder(SqlDataReader reader)
        {
            ToDoFolder tdf = new ToDoFolder
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["folder_name"]),
                CreatedDate = Convert.ToDateTime(reader["created_date"]),
                SortField = Convert.ToString(reader["sort_field"]),
                SortOrder = Convert.ToString(reader["sort_order"]),
                FolderItems = new List<ToDoItem>()
            };

            return tdf;
        }

        public ToDoFolder Create(ToDoFolder folderToCreate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                //Open the connection
                conn.Open();

                //sql command to execute, AND the connection
                SqlCommand cmd = new SqlCommand("folder_create", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", folderToCreate.Name);
                cmd.Parameters.AddWithValue("@createddate", folderToCreate.CreatedDate);
                cmd.Parameters.AddWithValue("@sortorder", folderToCreate.SortOrder);
                cmd.Parameters.AddWithValue("@sortfield", folderToCreate.SortField);

                //int numRowsAffected = cmd.ExecuteNonQuery();
                int id = Convert.ToInt32(cmd.ExecuteScalar());

                //cmd = new SqlCommand("SELECT MAX(id) FROM todo_folder;", conn);
                //conn.Close(); //need to explicitly close conn to call next method
                //int id = 0; // GetMaxId();

                conn.Close(); //need to explicitly close conn to call next method

                ToDoFolder fromDB = Get(id);

                if (fromDB == folderToCreate)
                {
                    return fromDB;
                }
                else
                {
                    Console.WriteLine("Error saving todo folder.");
                    return null;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error saving todo folder.");
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public bool Delete(int idToDelete)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                //Open the connection
                conn.Open();

                //sql command to execute, AND the connection
                SqlCommand cmd = new SqlCommand("folder_delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", idToDelete);

                int numRowsAffected = cmd.ExecuteNonQuery();

                conn.Close();  //need to explicitly close conn to call next method

                ToDoFolder fromDB = Get(idToDelete);

                if (numRowsAffected == 1 && fromDB == null)
                    return true;
                else
                    Console.WriteLine("Error deleting todo folder.");
                return false;

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error deleting todo folder.");
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public ToDoFolder Get(int id)
        {
            ToDoFolder tdf = new ToDoFolder();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get the todo folder
                SqlCommand cmd = new SqlCommand("folder_get_by_id", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //create a new todo folder object and return it
                    tdf = ConvertReaderToToDoFolder(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting todo folder.");
                Console.WriteLine(e.Message);
                throw;
            }
            return tdf;
        }

        public ToDoFolder Get(string name)
        {
            ToDoFolder tdf = new ToDoFolder();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get the todo folder
                SqlCommand cmd = new SqlCommand("folder_get_by_name", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //create a new todo folder object and return it
                    tdf = ConvertReaderToToDoFolder(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting todo folder.");
                Console.WriteLine(e.Message);
                throw;
            }
            return tdf;
        }

        public IList<ToDoFolder> List()
        {
            List<ToDoFolder> list = new List<ToDoFolder>();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get all of the todo folders
                SqlCommand cmd = new SqlCommand("folder_get_all", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //for every row, create a new todo folder object and add it to the list
                    ToDoFolder tdf = ConvertReaderToToDoFolder(reader);
                    list.Add(tdf);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting todo folders.");
                Console.WriteLine(e.Message);
                throw;
            }
            return list;
        }

        public ToDoFolder Update(ToDoFolder updatedToDoFolder)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                //Open the connection
                conn.Open();

                //sql command to execute, AND the connection
                SqlCommand cmd = new SqlCommand("folder_update", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", updatedToDoFolder.Id);
                cmd.Parameters.AddWithValue("@name", updatedToDoFolder.Name);
                cmd.Parameters.AddWithValue("@sortfield", updatedToDoFolder.SortField);
                cmd.Parameters.AddWithValue("@sortorder", updatedToDoFolder.SortOrder);

                int numRowsAffected = cmd.ExecuteNonQuery();

                conn.Close(); //need to explicitly close conn to call next method
                ToDoFolder fromDB = Get(updatedToDoFolder.Id);

                if (fromDB == updatedToDoFolder && numRowsAffected == 1)
                {
                    return fromDB;
                }
                else
                {
                    return null;
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error saving todo folder.");
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}