using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public class ToDoItemSqlDAO : IToDoItemDAO
    {

        private string connectionString;

        // Single Parameter Constructor
        public ToDoItemSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public ToDoItem ConvertReaderToToDoItem(SqlDataReader reader)
        {
            ToDoItem tdi = new ToDoItem
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["item_name"]),
                DueDate = Convert.ToDateTime(reader["due_date"]),
                Reminder = Convert.ToDateTime(reader["reminder"]),
                CreatedDate = Convert.ToDateTime(reader["created_date"]),
                IsComplete = Convert.ToBoolean(reader["is_complete"]),
                IsImportant = Convert.ToBoolean(reader["is_important"]),
                Recurrence = Convert.ToString(reader["item_recurrence"]),
                FolderName = Convert.ToString(reader["folder_name"])
            };

            return tdi;
        }

        public ToDoItem Create(ToDoItem itemToCreate)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                //Open the connection
                conn.Open();

                //sql command to execute, AND the connection
                SqlCommand cmd = new SqlCommand("item_create", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", itemToCreate.Name);
                cmd.Parameters.AddWithValue("@duedate", itemToCreate.DueDate);
                cmd.Parameters.AddWithValue("@reminder", itemToCreate.Reminder);
                cmd.Parameters.AddWithValue("@createddate", itemToCreate.CreatedDate);
                cmd.Parameters.AddWithValue("@itemrecurrence", itemToCreate.Recurrence);
                cmd.Parameters.AddWithValue("@iscomplete", itemToCreate.IsComplete);
                cmd.Parameters.AddWithValue("@isimportant", itemToCreate.IsImportant);
                cmd.Parameters.AddWithValue("@foldername", itemToCreate.FolderName);

                //int numRowsAffected = cmd.ExecuteNonQuery();
                int id = Convert.ToInt32(cmd.ExecuteScalar());

                //cmd = new SqlCommand("SELECT MAX(todo_item_id) FROM todo_item;", conn);
                //cmd = new SqlCommand("SELECT SCOPE_IDENTITY() FROM todo_item;", conn);
                //int id = Convert.ToInt32(cmd.ExecuteScalar());

                //conn.Close();  //need to explicitly close conn to call next method
                //int id = 0; // GetMaxId();

                conn.Close();  //need to explicitly close conn to call next method
                
                ToDoItem fromDB = Get(id);

                if (fromDB == itemToCreate)
                {
                    return fromDB;
                }
                else
                {
                    Console.WriteLine("Error saving todo item.");
                    return null;
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error saving todo item.");
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
                SqlCommand cmd = new SqlCommand("item_delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", idToDelete);

                int numRowsAffected = cmd.ExecuteNonQuery();

                conn.Close();  //need to explicitly close conn to call next method

                ToDoItem fromDB = Get(idToDelete);

                if (numRowsAffected == 1 && fromDB == null)
                    return true;
                else
                    Console.WriteLine("Error deleting todo item.");
                return false;

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error deleting todo item.");
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public ToDoItem Get(int id)
        {
            ToDoItem tdi = new ToDoItem();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get all of the todo items
                SqlCommand cmd = new SqlCommand("item_get_by_id", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    tdi = ConvertReaderToToDoItem(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting todo items.");
                Console.WriteLine(e.Message);
                throw;
            }
            return tdi;
        }

        public IList<ToDoItem> List(string foldername)
        {
            List<ToDoItem> list = new List<ToDoItem>();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get all of the todo items
                SqlCommand cmd = new SqlCommand("item_get_by_folder_name", conn);
                cmd.Parameters.AddWithValue("@name", foldername);
                cmd.CommandType = CommandType.StoredProcedure;

                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //for every row, create a new todo item object and add it to the list
                    ToDoItem tdi = ConvertReaderToToDoItem(reader);
                    list.Add(tdi);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting todo items.");
                Console.WriteLine(e.Message);
                throw;
            }
            return list;
        }

        public IList<ToDoItem> List()
        {
            List<ToDoItem> list = new List<ToDoItem>();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get all of the todo items
                SqlCommand cmd = new SqlCommand("item_get_all", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //for every row, create a new todo item object and add it to the list
                    ToDoItem tdi = ConvertReaderToToDoItem(reader);
                    list.Add(tdi);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting todo items.");
                Console.WriteLine(e.Message);
                throw;
            }
            return list;
        }

        public ToDoItem Update(ToDoItem updatedToDoItem)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                //Open the connection
                conn.Open();

                //sql command to execute, AND the connection
                SqlCommand cmd = new SqlCommand("item_update", conn);
                cmd.Parameters.AddWithValue("@id", updatedToDoItem.Id);
                cmd.Parameters.AddWithValue("@name", updatedToDoItem.Name);
                cmd.Parameters.AddWithValue("@itemrecurrence", updatedToDoItem.Recurrence);
                cmd.Parameters.AddWithValue("@reminder", updatedToDoItem.Reminder);
                cmd.Parameters.AddWithValue("@duedate", updatedToDoItem.DueDate);
                cmd.Parameters.AddWithValue("@iscomplete", updatedToDoItem.IsComplete);
                cmd.Parameters.AddWithValue("@isimportant", updatedToDoItem.IsImportant);
                cmd.Parameters.AddWithValue("@foldername", updatedToDoItem.FolderName);
                cmd.CommandType = CommandType.StoredProcedure;
                
                int numRowsAffected = cmd.ExecuteNonQuery();

                conn.Close(); //need to explicitly close conn to call next method
                
                ToDoItem fromDB = Get(updatedToDoItem.Id);

                if (fromDB == updatedToDoItem && numRowsAffected == 1)
                {
                    return fromDB;
                }
                else
                {
                    Console.WriteLine("Error saving todo item.");
                    return null;
                }                
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error saving todo item.");
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
