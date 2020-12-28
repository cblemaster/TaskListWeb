using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ToDoList.Models
{
    /// <summary>
    /// Class to represent a todo item
    /// </summary>
    public class ToDoItem
    {
        /// <summary>
        /// The unique id of the todo item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the todo item
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Max length is 50 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// The due date of the todo item
        /// </summary>
        public SqlDateTime DueDate { get; set; }   //NEEDS VALIDATION - must be today or later

        /// <summary>
        /// The reminder date/time of the todo item
        /// </summary>
        public SqlDateTime Reminder { get; set; }  //NEEDS VALIDATION - must be due date or later

        /// <summary>
        /// The created date for the todo item
        /// </summary>
        public SqlDateTime CreatedDate { get; set; }

        /// <summary>
        /// The recurrence selection for the todo item (daily, weekly, weekdays, monthly, annually, none)
        /// </summary>
        [MaxLength(10, ErrorMessage = "Max length is 10 characters.")]
        public string Recurrence { get; set; }

        /// <summary>
        /// True/false whether the todo item is complete
        /// </summary>
        public SqlBoolean IsComplete { get; set; }

        /// <summary>
        /// True/false whether the todo item is important
        /// </summary>
        public SqlBoolean IsImportant { get; set; }

        /// <summary>
        /// The folder name for the todo item
        /// </summary>
        [MaxLength(30, ErrorMessage = "Max length is 30 characters.")]
        public string FolderName { get; set; }

        /// <summary>
        /// Constructor for the ToDoItem class
        /// </summary>
        /// <param name="id">The unique id of the todo item</param>
        /// <param name="name">The name of the todo item</param>
        /// <param name="dueDate">The due date of the todo item</param>
        /// <param name="reminder">The reminder date/time of the todo item</param>
        /// <param name="recurrence">The recurrence selection for the todo item (values daily, weekly, weekdays, monthly, annually, none)</param>
        public ToDoItem(int id, string name, DateTime dueDate, DateTime reminder, string recurrence, string foldername)
        {
            Id = id;
            Name = name;
            DueDate = dueDate;
            Reminder = reminder;
            Recurrence = recurrence;
            FolderName = foldername;
            CreatedDate = DateTime.Now;
            IsComplete = false;
            IsImportant = false;            
        }

        /// <summary>
        /// "No arg" constructor for the ToDoItem class
        /// </summary>
        public ToDoItem()
        {
        }
    }
}
