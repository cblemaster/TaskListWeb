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
    public class ItemsController : ControllerBase
    {

        private static IToDoItemDAO itemDAO;

        public ItemsController(IToDoItemDAO _itemDAO)
        {
            itemDAO = _itemDAO;
        }

        // GET: api/<ItemsController>
        [HttpGet]
        public IList<ToDoItem> Get()
        {
            return itemDAO.List();
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<ToDoItem> Get(int id)
        {
            ToDoItem tdi = itemDAO.Get(id);
            if (tdi != null)
            {
                return tdi;
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<FoldersController>/Tasks
        [HttpGet("{name}")]
        public IList<ToDoItem> Get(string name)
        {
            return itemDAO.List(name);
        }

        // POST api/<ItemsController>
        [HttpPost]
        public ActionResult<ToDoItem> AddItem(ToDoItem itemToAdd)
        {
            ToDoItem added = itemDAO.Create(itemToAdd);
            return Created($"{itemToAdd.Id}", added);
        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public ActionResult<ToDoItem> UpdateItem(ToDoItem itemToUpdate)
        {
            ToDoItem existing = itemDAO.Get(itemToUpdate.Id);
            if (existing == null)
            {
                return NotFound("Item not found.");
            }
            else
            {
                ToDoItem result = itemDAO.Update(itemToUpdate);
                return Ok(result);
            }
        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteFolder(int id)
        {
            ToDoItem deleted = itemDAO.Get(id);
            if (deleted == null)
            {
                return NotFound("Item not found.");
            }
            else
            {
                bool result = itemDAO.Delete(id);
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
