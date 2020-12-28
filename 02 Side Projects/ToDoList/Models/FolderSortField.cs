using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    /// <summary>
    /// Class to represent a sort field selection for a folder of todo items    
    /// </summary>
    public class FolderSortField
    {
        /// <summary>
        /// The unique id of the sort field selection for a folder of todo items
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the sort field selection for a folder of todo items
        /// Possible values are  name,  due_date, none
        /// </summary>
        [Required]
        [MaxLength(10, ErrorMessage = "Max length is 10 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Constructor for the FolderSortField class
        /// </summary>
        /// <param name="id">The unique id of the sort field selection for a folder of todo items</param>
        /// <param name="name">The name of the sort field selection for a folder of todo items (name, due_date, none)</param>
        public FolderSortField(int id,  string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// "No arg" constructor for the FolderSortField class
        /// </summary>
        public FolderSortField()
        {

        }
    }
}
