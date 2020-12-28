using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    /// <summary>
    /// Class to represent a sort order selection for a folder of todo items    
    /// </summary>
    public class FolderSortOrder
    {
        /// <summary>
        /// The unique id of the sort order selection for a folder of todo items
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the sort order selection for a folder of todo items
        /// Possible values are asc, desc, none
        /// </summary>
        [Required]
        [MaxLength(4, ErrorMessage = "Max length is 4 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Constructor for the FolderSortOrder class
        /// </summary>
        /// <param name="id">The unique id of the sort order selection for a folder of todo items</param>
        /// <param name="name">The name of the sort order selection for a folder of todo items (asc, desc, none)</param>
        public FolderSortOrder(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// "No arg" constructor for the FolderSortOrder class
        /// </summary>
        public FolderSortOrder()
        {

        }
    }
}
