using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaskListWeb.DAL;
using TaskListWeb.Models;

namespace TaskListWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class FolderController : ControllerBase
    {

        private readonly IFolderDAO folderDAO;

        public FolderController(IFolderDAO _folderDAO)
        {
            folderDAO = _folderDAO;
        }

        // https://localhost:44312/folder/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Folder f = folderDAO.Get(id);
            if (f == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(f);
            }
        }

        // https://localhost:44312/folder
        [HttpGet]
        public IActionResult List()
        {
            List<Folder> folders = folderDAO.List();
            if (folders == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(folders);
            }
        }

        // https://localhost:44312/folder/desc
        [HttpGet("desc")]
        public IActionResult ListSortDesc()
        {
            List<Folder> folders = folderDAO.ListSortDesc();
            if (folders == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(folders);
            }
        }

        // https://localhost:44312/folder/asc
        [HttpGet("asc")]
        public IActionResult ListSortAsc()
        {
            List<Folder> folders = folderDAO.ListSortAsc();
            if (folders == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(folders);
            }
        }

        // https://localhost:44312/folder
        [HttpPost]
        public IActionResult Create(Folder folderToCreate)
        {
            Folder createdFolder = folderDAO.Create(folderToCreate);
            return Created($"{createdFolder.FolderId}", createdFolder);
        }

        // https://localhost:44312/folder/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (folderDAO.Delete(id))
            {
                return NoContent();
            }
            else
            {
                return BadRequest(); 
            }
        }

        // https://localhost:44312/folder/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Folder folderToUpdate)
        {
            Folder updatedFolder = folderDAO.Update(id, folderToUpdate);
            if (updatedFolder == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(updatedFolder);
            }
        }
    }
}