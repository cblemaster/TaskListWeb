using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.DAL
{
    interface IItemRecurrenceDAO
    {
        /// <summary>
        /// Creates an item recurrence object and sets it's properties to values read from the db
        /// </summary>
        /// <param name="reader">SQL data reader</param>
        /// <returns>The new item recurrence object</returns>
        ItemRecurrence ConvertReaderToItemRecurrence(SqlDataReader reader);
        
        /// <summary>
        /// Gets a list of all item recurrences
        /// </summary>
        /// <returns></returns>
        IList<ItemRecurrence> List();
        
        /// <summary>
        /// Gets a single item recurrence by it's id
        /// </summary>
        /// <param name="id">The id of the item recurrence to get</param>
        /// <returns></returns>
        ItemRecurrence Get(int id);

        /// <summary>
        /// Gets a single item recurrence by it's name
        /// </summary>
        /// <param name="name">The name of the item recurrence to get</param>
        /// <returns></returns>
        ItemRecurrence Get(string name);

    }
}
