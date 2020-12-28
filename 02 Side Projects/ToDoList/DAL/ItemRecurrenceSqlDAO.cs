using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public class ItemRecurrenceSqlDAO : IItemRecurrenceDAO
    {
        
        private string connectionString;

        // Single Parameter Constructor
        public ItemRecurrenceSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public ItemRecurrence ConvertReaderToItemRecurrence(SqlDataReader reader)
        {
            ItemRecurrence ir = new ItemRecurrence
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = Convert.ToString(reader["item_recurrence"])
            };

            return ir;
        }

        public ItemRecurrence Get(int id)
        {
            ItemRecurrence ir = new ItemRecurrence();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get the item recurrence
                SqlCommand cmd = new SqlCommand("item_recurrence_get_by_id", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //create a new item_recurrence object and return it
                    ir = ConvertReaderToItemRecurrence(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting item recurrence.");
                Console.WriteLine(e.Message);
                throw;
            }
            return ir;
        }

        public ItemRecurrence Get(string name)
        {
            ItemRecurrence ir = new ItemRecurrence();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get the item recurrence
                SqlCommand cmd = new SqlCommand("item_recurrence_get_by_name", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //create a new item_recurrence object and return it
                    ir = ConvertReaderToItemRecurrence(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting item recurrence.");
                Console.WriteLine(e.Message);
                throw;
            }
            return ir;
        }

        public IList<ItemRecurrence> List()
        {
            List<ItemRecurrence> list = new List<ItemRecurrence>();
            try
            {
                //create a sql connection
                using SqlConnection conn = new SqlConnection(connectionString);
                //open it
                conn.Open();
                // create a sql command to get all of the item recurrences
                SqlCommand cmd = new SqlCommand("item_recurrence_get_all", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //   execute that
                SqlDataReader reader = cmd.ExecuteReader();
                //  read the results
                while (reader.Read())
                {
                    //for every row, create a new item recurrence object and add it to the list
                    ItemRecurrence ir = ConvertReaderToItemRecurrence(reader);
                    list.Add(ir);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting item recurrences.");
                Console.WriteLine(e.Message);
                throw;
            }
            return list;
        }
    }
}