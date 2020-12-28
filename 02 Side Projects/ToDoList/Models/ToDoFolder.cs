using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ToDoList.Models
{
    /// <summary>
    /// Class to represent a folder of todo items    
    /// </summary>
    public class ToDoFolder
    {
        /// <summary>
        /// The unique id of the folder of todo items
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the folder of todo items
        /// </summary>
        [Required]
        [MaxLength(30, ErrorMessage = "Max length is 30 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// The sort field selection for the folder of todo items
        /// Possible values are name, due_date, none
        /// </summary>
        [MaxLength(10, ErrorMessage = "Max length is 10 characters.")]
        public string SortField { get; set; }

        /// <summary>
        /// The sort order selection for the folder of todo items
        /// Possible values are asc, desc, none
        /// </summary>
        [MaxLength(4, ErrorMessage = "Max length is 4 characters.")]
        public string SortOrder { get; set; }

        /// <summary>
        /// The created date for the folder of todo items
        /// </summary>
        public SqlDateTime CreatedDate { get; set; }

        /// <summary>
        /// The todo items in the folder of todo items
        /// </summary>
        public List<ToDoItem> FolderItems { get; set; }

        /// <summary>
        /// Constructor for the ToDoFolder class
        /// </summary>
        /// <param name="id">The unique id of the folder of todo items</param>
        /// <param name="name">The name of the folder of todo items</param>
        /// <param name="sortField">The sort field selection for the folder of todo items (todo name, todo due date)</param>
        /// <param name="sortOrder">The sort order selection for the folder of todo items (asc, desc)</param>
        public ToDoFolder(int id, string name, string sortField, string sortOrder)
        {
            Id = id;
            Name = name;
            SortField = sortField;
            SortOrder = sortOrder;
            CreatedDate = DateTime.Now;
            FolderItems = new List<ToDoItem>();
        }
        /// <summary>
        /// "No arg" constructor for the ToDoFolder class
        /// </summary>
        public ToDoFolder()
        {

        }

    }
}