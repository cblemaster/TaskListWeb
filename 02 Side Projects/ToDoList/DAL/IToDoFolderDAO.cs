using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public interface IToDoFolderDAO
    {
        
        /// <summary>
        /// Creates a todo folder object and sets it's properties to values read from the db
        /// </summary>
        /// <param name="reader">SQL data reader</param>
        /// <returns>The new todo folder object</returns>
        ToDoFolder ConvertReaderToToDoFolder(SqlDataReader reader);

        /// <summary>
        /// Gets a list of all todo folders
        /// </summary>
        /// <returns></returns>
        IList<ToDoFolder> List();

        /// <summary>
        /// Gets a single todo folder by it's id
        /// </summary>
        /// <param name="id">The id of the todo folder to get</param>
        /// <returns></returns>
        ToDoFolder Get(int id);

        /// <summary>
        /// Gets a single todo folder by it's name
        /// </summary>
        /// <param name="name">The name of the todo folder to get</param>
        /// <returns></returns>
        ToDoFolder Get(string name);

        /// <summary>
        /// Creates a new todo folder
        /// </summary>
        /// <param name="folderToCreate">The new todo folder to create</param>
        /// <returns>The newly created todo folder object</returns>
        ToDoFolder Create(ToDoFolder folderToCreate);
        
        /// <summary>
        /// Deletes a todo folder
        /// </summary>
        /// <param name="idToDelete">The id of the todo folder to delete</param>
        /// <returns>true if the delete succeeded, false if the delete did not succeed</returns>
        bool Delete(int idToDelete);

        /// <summary>
        /// Updates a todo folder
        /// </summary>
        /// <param name="updatedToDoFolder">The updated todo folder</param>
        /// <returns>The updated todo folder</returns>
        ToDoFolder Update(ToDoFolder updatedToDoFolder);
        
    }
}
