using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.DAL
{
    interface IFolderSortFieldDAO
    {
        /// <summary>
        /// Creates a folder sort field object and sets it's properties to values read from the db
        /// </summary>
        /// <param name="reader">SQL data reader</param>
        /// <returns>The new folder sort field object</returns>
        FolderSortField ConvertReaderToFolderSortField(SqlDataReader reader);
        
        /// <summary>
        /// Gets a list of all folder sort fields
        /// </summary>
        /// <returns></returns>
        IList<FolderSortField> List();
        
        /// <summary>
        /// Gets a single folder sort field by it's id
        /// </summary>
        /// <param name="id">The id of the folder sort field to get</param>
        /// <returns></returns>
        FolderSortField Get(int id);

        /// <summary>
        /// Gets a single folder sort field by it's name
        /// </summary>
        /// <param name="name">The name of the folder sort field to get</param>
        /// <returns></returns>
        FolderSortField Get(string name);

    }
}
