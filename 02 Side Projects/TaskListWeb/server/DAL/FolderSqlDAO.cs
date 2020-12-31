using System;
using System.Collections.Generic;
using TaskListWeb.Models;
using System.Data.SqlClient;

namespace TaskListWeb.DAL
{
    public class FolderSqlDAO : IFolderDAO
    {
        private readonly string connectionString;

        public FolderSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Folder Create(Folder folderToCreate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO folder " +
                                                    "(folder_name, created_date) " +
                                                    "VALUES (@foldername, GETDATE())", conn);
                    cmd.Parameters.AddWithValue("@foldername", folderToCreate.FolderName);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(folder_id) FROM folder", conn);
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM folder " +
                                                    "WHERE folder_id=@folderid", conn);
                    cmd.Parameters.AddWithValue("@folderid", idToDelete);
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

        public Folder Get(int folderId)
        {
            Folder f = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT folder_id, folder_name, created_date " +
                                                "FROM folder " +
                                                "WHERE folder_id=@folderid", conn);
                    cmd.Parameters.AddWithValue("@folderid", folderId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows && reader.Read())
                    {
                        f = ConvertReaderToFolder(reader);
                    }
                }
            }

            catch (SqlException e)
            {
                throw;
            }

            return f;
        }

        public List<Folder> List()
        {
            List<Folder> folders = new List<Folder>();
            Folder f = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT folder_id, folder_name, created_date " +
                                                "FROM folder", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        f = ConvertReaderToFolder(reader);
                        folders.Add(f);
                    }
                }
            }

            catch (SqlException e)
            {
                throw;
            }

            return folders;
        }

        public Folder Update(int idToUpdate, Folder folderToUpdate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE folder set folder_name=@foldername " +
                                                    "WHERE folder_id=@folderid", conn);
                    cmd.Parameters.AddWithValue("@foldername", folderToUpdate.FolderName);
                    cmd.Parameters.AddWithValue("@folderid", folderToUpdate.FolderId);

                    cmd.ExecuteNonQuery();

                    return Get(folderToUpdate.FolderId);
                }
            }

            catch (SqlException e)
            {
                throw;
            }
        }

        public Folder ConvertReaderToFolder(SqlDataReader reader)
        {
            Folder f = new Folder
            {
                FolderId = Convert.ToInt32(reader["folder_id"]),
                FolderName = Convert.ToString(reader["folder_name"]),
                CreatedDate = Convert.ToDateTime(reader["created_date"])
            };

            return f;
        }

        public List<Folder> ListSortAsc()
        {
            List<Folder> folders = new List<Folder>();
            Folder f = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT folder_id, folder_name, created_date " +
                                                "FROM folder " +
                                                "ORDER BY folder_name ASC", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        f = ConvertReaderToFolder(reader);
                        folders.Add(f);
                    }
                }
            }

            catch (SqlException e)
            {
                throw;
            }

            return folders;
        }

        public List<Folder> ListSortDesc()
        {
            List<Folder> folders = new List<Folder>();
            Folder f = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT folder_id, folder_name, created_date " +
                                                "FROM folder " +
                                                "ORDER BY folder_name DESC", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        f = ConvertReaderToFolder(reader);
                        folders.Add(f);
                    }
                }
            }

            catch (SqlException e)
            {
                throw;
            }

            return folders;
        }
    }
}