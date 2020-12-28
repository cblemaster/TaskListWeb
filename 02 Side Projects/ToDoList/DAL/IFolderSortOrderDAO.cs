using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.DAL
{
    interface IFolderSortOrderDAO
    {
        /// <summary>
        /// Creates a folder sort order object and sets it's properties to values read from the db
        /// </summary>
        /// <param name="reader">SQL data reader</param>
        /// <returns>The new folder sort order object</returns>
        FolderSortOrder ConvertReaderToFolderSortOrder(SqlDataReader reader);
        
        /// <summary>
        /// Gets a list of all folder sort orders
        /// </summary>
        /// <returns></returns>
        IList<FolderSortOrder> List();
        
        /// <summary>
        /// Gets a single folder sort order by it's id
        /// </summary>
        /// <param name="id">The id of the folder sort order to get</param>
        /// <returns></returns>
        FolderSortOrder Get(int id);

        /// <summary>
        /// Gets a single folder sort order by it's name
        /// </summary>
        /// <param name="name">The name of the folder sort order to get</param>
        /// <returns></returns>
        FolderSortOrder Get(string name);

    }
}
