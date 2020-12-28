using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.DAL;
using ToDoList.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private static IToDoFolderDAO folderDAO;

        public FoldersController(IToDoFolderDAO _folderDAO)
        {
            folderDAO = _folderDAO;
        }

        
        // GET: api/folders
        [HttpGet]
        public IList<ToDoFolder> Get()
        {
            return folderDAO.List();
        }

        // GET api/folders/byid/5
        [HttpGet("id/{id}")]
        public ActionResult<ToDoFolder> Get(int id)
        {
            ToDoFolder tdf = folderDAO.Get(id);
            if (tdf != null)
            {
                return tdf;
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/folders/Tasks
        [HttpGet("name/{name}")]
        public ActionResult<ToDoFolder> Get(string name)
        {
            ToDoFolder tdf = folderDAO.Get(name);
            if (tdf != null)
            {
                return tdf;
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/Folders
        [HttpPost]
        public ActionResult<ToDoFolder> AddFolder(ToDoFolder folderToAdd)
        {
            ToDoFolder added = folderDAO.Create(folderToAdd);
            return Created($"{folderToAdd.Id}", added);
        }

        // PUT api/<FoldersController>/5
        [HttpPut("{id}")]
        public ActionResult<ToDoFolder> UpdateFolder(ToDoFolder folderToUpdate)
        {
            ToDoFolder existing = folderDAO.Get(folderToUpdate.Id);
            if (existing == null)
            {
                return NotFound("Folder not found.");
            }
            else
            {
                ToDoFolder result = folderDAO.Update(folderToUpdate);
                return Ok(result);
            }
        }

        // DELETE api/<FoldersController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteFolder(int id)
        {
            ToDoFolder deleted = folderDAO.Get(id);
            if (deleted == null)
            {
                return NotFound("Folder not found.");
            }
            else
            {
                bool result = folderDAO.Delete(id);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }               
            }
        }
    }
}