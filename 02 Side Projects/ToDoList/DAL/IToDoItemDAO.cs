using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public interface IToDoItemDAO
    {
        /// <summary>
        /// Creates a todo item object and sets it's properties to values read from the db
        /// </summary>
        /// <param name="reader">SQL data reader</param>
        /// <returns>The new todo item object</returns>
        ToDoItem ConvertReaderToToDoItem(SqlDataReader reader);

        /// <summary>
        /// Gets a list of all todo items
        /// </summary>
        /// <returns></returns>
        IList<ToDoItem> List();
        
        /// <summary>
        /// Gets a single todo item by it's id
        /// </summary>
        /// <param name="id">The id of the todo item to get</param>
        /// <returns></returns>
        ToDoItem Get(int id);

        /// <summary>
        /// Gets a list of all todo items by folder name
        /// </summary>
        /// <returns></returns>
        IList<ToDoItem> List(string foldername);

        /// <summary>
        /// Creates a new todo item
        /// </summary>
        /// <param name="itemToCreate">The new todo item to create</param>
        /// <returns>The newly created todo item object</returns>
        ToDoItem Create(ToDoItem itemToCreate);

        /// <summary>
        /// Deletes a todo item
        /// </summary>
        /// <param name="idToDelete">The id of the todo item to delete</param>
        /// <returns>true if the delete succeeded, false if the delete did not succeed</returns>
        bool Delete(int idToDelete);

        /// <summary>
        /// Updates a todo item
        /// </summary>
        /// <param name="updatedToDoItem">The updated todo item</param>
        /// <returns>The updated todo item</returns>
        ToDoItem Update(ToDoItem updatedToDoItem);        
    }
}
