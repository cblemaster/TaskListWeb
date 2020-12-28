using System;
using System.Collections.Generic;
using TaskListWeb.Models;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace TaskListWeb.DAL
{
    public class TaskSqlDAO : ITaskDAO
    {
        private readonly string connectionString;

        public TaskSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Task Create(Models.Task taskToCreate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO task " +
                                                    "(task_name, due_date, reminder, created_date, " +
                                                    "recurrence_id, is_complete, is_important, folder_id) " +
                                                    "VALUES (@taskname, @duedate, @reminder, " +
                                                    "GETDATE(), " +
                                                    "(SELECT recurrence_id FROM recurrence WHERE recurrence_name=@recurrencename), @iscomplete, " +
                                                    "@isimportant, (SELECT folder_id FROM folder WHERE folder_name=@foldername)", conn);
                    
                    cmd.Parameters.AddWithValue("@taskname", taskToCreate.TaskName);
                    cmd.Parameters.AddWithValue("@duedate", (SqlDateTime)taskToCreate.DueDate);
                    cmd.Parameters.AddWithValue("@reminder", (SqlDateTime)taskToCreate.Reminder);
                    cmd.Parameters.AddWithValue("@recurrencename", taskToCreate.RecurrenceName);
                    cmd.Parameters.AddWithValue("@iscomplete", (SqlBoolean)taskToCreate.IsComplete);
                    cmd.Parameters.AddWithValue("@isimportant", (SqlBoolean)taskToCreate.IsImportant);
                    cmd.Parameters.AddWithValue("@folderid", taskToCreate.FolderName);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(task_id) FROM task", conn);
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    return Get(newId);

                }
            }

            catch (SqlException e)
            {
                throw;
            }
        }

        public bool Delete(int idToDelete)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM task " +
                                                    "WHERE task_id=@taskid", conn);
                    cmd.Parameters.AddWithValue("@taskid", idToDelete);
                    int rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                    if (rowsAffected != 1)
                    {
                        return false;
                    }

                    return true;

                }
            }

            catch (SqlException e)
            {
                throw;
            }
        }

        public Task Get(int taskId)
        {
            Task t = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT task_id, task_name, task.created_date AS created_date, " +
                                                    "due_date, reminder, is_complete, " +
                                                    "is_important, " +
                                                    "folder_name, recurrence_name " +
                                                    "FROM task " +
                                                    "INNER JOIN folder ON folder.folder_id = task.folder_id " +
                                                    "INNER JOIN recurrence ON recurrence.recurrence_id = task.recurrence_id " +
                                                    "WHERE task_id=@taskid", conn);
                    cmd.Parameters.AddWithValue("@taskid", taskId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        t = ConvertReaderToTask(reader);
                    }
                }
            }

            catch (SqlException e)
            {
                throw;
            }

            return t;
        }

        public List<Task> List()
        {
            List<Task> tasks = new List<Task>();
            Task t = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT task_id, task_name, task.created_date AS created_date, " +
                                                    "due_date, reminder, is_complete, " +
                                                    "is_important, " +
                                                    "folder_name, recurrence_name " +
                                                    "FROM task " +
                                                    "INNER JOIN folder ON folder.folder_id = task.folder_id " +
                                                    "INNER JOIN recurrence ON recurrence.recurrence_id = task.recurrence_id ", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        t = ConvertReaderToTask(reader);
                        tasks.Add(t);
                    }
                }

            }
            catch (SqlException e)
            {
                throw;
            }

            return tasks;
        }

        public List<Task> ListByFolderId(int folderId)
        {
            List<Task> tasks = new List<Task>();
            Task t = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT task_id, task_name, task.created_date AS created_date, " +
                                                    "due_date, reminder, is_complete, " +
                                                    "is_important, " +
                                                    "folder_name, recurrence_name " +
                                                    "FROM task " +
                                                    "INNER JOIN folder ON folder.folder_id = task.folder_id " +
                                                    "INNER JOIN recurrence ON recurrence.recurrence_id = task.recurrence_id " +
                                                    "WHERE task.folder_id=@folderid", conn);
                    cmd.Parameters.AddWithValue("@folderid", folderId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        t = ConvertReaderToTask(reader);
                        tasks.Add(t);
                    }
                }

            }
            catch (SqlException e)
            {
                throw;
            }

            return tasks;
        }

        public Task Update(int idToUpdate, Models.Task taskToUpdate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE task SET task_name=@taskname, " +
                                                    "due_date=@duedate, reminder=@reminder, " +
                                                    "recurrence_id=(SELECT recurrence_id FROM recurrence WHERE recurrence_name=@recurrencename), " +
                                                    "is_complete=@iscomplete, is_important=@isimportant, " +
                                                    "folder_id=(SELECT folder_id FROM folder WHERE folder_name=@foldername) " +
                                                    "WHERE task_id=@taskid", conn);
                    cmd.Parameters.AddWithValue("@taskname", taskToUpdate.TaskName);
                    cmd.Parameters.AddWithValue("@duedate", taskToUpdate.DueDate);
                    cmd.Parameters.AddWithValue("@reminder", taskToUpdate.Reminder);
                    cmd.Parameters.AddWithValue("@recurrencename", taskToUpdate.RecurrenceName);
                    cmd.Parameters.AddWithValue("@iscomplete", taskToUpdate.IsComplete);
                    cmd.Parameters.AddWithValue("@isimportant", taskToUpdate.IsImportant);
                    cmd.Parameters.AddWithValue("@foldername", taskToUpdate.FolderName);
                    cmd.Parameters.AddWithValue("@taskid", taskToUpdate.TaskId);

                    cmd.ExecuteNonQuery();

                    return Get(taskToUpdate.TaskId);
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public Task ConvertReaderToTask(SqlDataReader reader)
        {
            Task t = new Task
            {
                TaskId = Convert.ToInt32(reader["task_id"]),
                TaskName = Convert.ToString(reader["task_name"]),
                DueDate = Convert.ToDateTime(reader["due_date"]),
                Reminder = Convert.ToDateTime(reader["reminder"]),
                CreatedDate = Convert.ToDateTime(reader["created_date"]),
                IsComplete = Convert.ToBoolean(reader["is_complete"]),
                IsImportant = Convert.ToBoolean(reader["is_important"]),
                RecurrenceName = Convert.ToString(reader["recurrence_name"]),
                FolderName = Convert.ToString(reader["folder_name"])
            };

            return t;
        }

        public List<Task> ListImportant()
        {
            List<Task> tasks = new List<Task>();
            Task t = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT task_id, task_name, task.created_date AS created_date, " +
                                                    "due_date, reminder, task.recurrence_id AS recurrence_id, is_complete, " +
                                                    "is_important, task.folder_id AS folder_id, " +
                                                    "folder_name, recurrence_name " +
                                                    "FROM task " +
                                                    "INNER JOIN folder ON folder.folder_id = task.folder_id " +
                                                    "INNER JOIN recurrence ON recurrence.recurrence_id = task.recurrence_id " +
                                                    "WHERE is_important = 1", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        t = ConvertReaderToTask(reader);
                        tasks.Add(t);
                    }
                }

            }
            catch (SqlException e)
            {
                throw;
            }

            return tasks;
        }

        public List<Task> ListRecurring()
        {
            List<Task> tasks = new List<Task>();
            Task t = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT task_id, task_name, task.created_date AS created_date, " +
                                                    "due_date, reminder, task.recurrence_id AS recurrence_id, is_complete, " +
                                                    "is_important, task.folder_id AS folder_id, " +
                                                    "folder_name, recurrence_name " +
                                                    "FROM task " +
                                                    "INNER JOIN folder ON folder.folder_id = task.folder_id " +
                                                    "INNER JOIN recurrence ON recurrence.recurrence_id = task.recurrence_id " +
                                                    "WHERE task.recurrence_id != 1", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        t = ConvertReaderToTask(reader);
                        tasks.Add(t);
                    }
                }

            }
            catch (SqlException e)
            {
                throw;
            }

            return tasks;
        }
    }
}