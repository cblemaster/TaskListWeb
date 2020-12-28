using System;
using System.ComponentModel.DataAnnotations;

namespace TaskListWeb.Models
{
    public class Folder
    {
        public int FolderId { get; set; }
        
        [Required]
        [MaxLength(50, ErrorMessage ="Max length for folder name is 50 characters.")]
        public string FolderName { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}