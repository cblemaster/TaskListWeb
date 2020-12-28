using System;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Microsoft.Data.SqlClient;

namespace ToDoListTests.DALIntegrationTests
{
    [TestClass]
    public class BaseToDoDAOTests
    {
        protected string ConnectionString { get; } = "Server = .; Database = ToDoList; Trusted_Connection = True";
        protected int NewSortFieldId { get; private set; }
        protected int NewSortOrderId { get; private set; }
        protected int NewItemRecurrenceId { get; private set; }
        protected int NewToDoFolderId { get; private set; }
        protected int NewToDoItemId { get; private set; }
        protected string NewToDoItemName { get; private set; }
        protected string NewSortFieldName { get; private set; }
        protected string NewSortOrderName { get; private set; }
        protected string NewItemRecurrence { get; private set; }
        protected string NewFolderName { get; private set; }


        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            // begin the transaction
            transaction = new TransactionScope();

            //get the SQL script to run
            string sql = File.ReadAllText("integration-test-script.sql");

            //execute the script
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                //if there are rows to read
                if (reader.Read())
                {
                    this.NewSortFieldId = Convert.ToInt32(reader["newSortFieldId"]);
                    this.NewSortOrderId = Convert.ToInt32(reader["newSortOrderId"]);
                    this.NewItemRecurrenceId = Convert.ToInt32(reader["newItemRecurrenceId"]);
                    this.NewToDoFolderId = Convert.ToInt32(reader["newToDoFolderId"]);
                    this.NewToDoItemId = Convert.ToInt32(reader["newToDoItemId"]);
                    this.NewToDoItemName = Convert.ToString(reader["newToDoItemName"]);
                    this.NewSortFieldName = Convert.ToString(reader["newSortFieldName"]);
                    this.NewSortOrderName = Convert.ToString(reader["newSortOrderName"]);
                    this.NewItemRecurrence = Convert.ToString(reader["newItemRecurrence"]);
                    this.NewFolderName = Convert.ToString(reader["newFolderName"]);
                }
            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            //roll back the transaction
            transaction.Dispose();
        }

        protected int GetTableRowCount(string tablename)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {tablename}", conn);
                int rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                return rowCount;
            }
        }

    }
}
