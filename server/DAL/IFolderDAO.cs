using System;
using System.Collections.Generic;
using TaskListWeb.Models;

namespace TaskListWeb.DAL
{
    public interface IFolderDAO
    {
        Folder Get(int folderId);
        List<Folder> List();
        Folder Create(Folder folderToCreate);
        bool Delete(int idToDelete);
        Folder Update(int idToUpdate, Folder folderToUpdate);
        List<Folder> ListSortAsc();
        List<Folder> ListSortDesc();
    }
}