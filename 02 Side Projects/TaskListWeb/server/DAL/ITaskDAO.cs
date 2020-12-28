using System;
using System.Collections.Generic;
using TaskListWeb.Models;

namespace TaskListWeb.DAL
{
    public interface ITaskDAO
    {
        Task Get(int taskId);
        List<Task> List();
        List<Task> ListByFolderId(int folderId);
        List<Task> ListImportant();
        List<Task> ListRecurring();
        Task Create(Task taskToCreate);
        bool Delete(int idToDelete);
        Task Update(int idToUpdate, Task taskToUpdate);
    }
}