using System;
using System.ComponentModel.DataAnnotations;


namespace TaskListWeb.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        
        [Required]
        [MaxLength(255,ErrorMessage = "Max length for task name is 255 characters.")]
        public string TaskName { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public DateTime Reminder { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        [Required]
        public bool IsComplete { get; set; }

        [Required]
        public bool IsImportant { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max length for folder name is 50 characters.")]
        public string FolderName { get; set; }
        
        [Required]
        [MaxLength(10, ErrorMessage = "Max length for recurrence is 10 characters.")]
        public string RecurrenceName { get; set; }

        [Required]
        public int FolderId { get; set; }

        [Required]
        public int RecurrenceId { get; set; }
    }
}