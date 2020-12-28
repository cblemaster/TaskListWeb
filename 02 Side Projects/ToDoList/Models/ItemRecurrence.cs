using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    /// <summary>
    /// Class to represent a todo item recurrence selection    
    /// </summary>
    public class ItemRecurrence
    {
        /// <summary>
        /// The unique id of the todo item recurrence selection
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the todo item recurrence selection
        /// Possible values are daily, weekdays, weekly, monthly, annually, none
        [Required]
        [MaxLength(10, ErrorMessage = "Max length is 10 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Constructor for the ItemRecurrence class
        /// </summary>
        /// <param name="id">The unique id of the todo item recurrence selection</param>
        /// <param name="name">The name of the todo item recurrence selection (daily, weekly, weekdays, monthly, annually, none)</param>
        public ItemRecurrence(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// "No arg" constructor for the ItemRecurrence class
        /// </summary>
        public ItemRecurrence()
        {
        }
    }
}