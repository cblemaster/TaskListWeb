using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaskListWeb.DAL;
using TaskListWeb.Models;

namespace TaskListWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class TaskController : ControllerBase
    {
        private readonly ITaskDAO taskDAO;

        public TaskController(ITaskDAO _taskDAO)
        {
            taskDAO = _taskDAO;
        }

        // https://localhost:44312/task/1
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Models.Task t = taskDAO.Get(id);
            if (t == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(t);
            }
        }

        // https://localhost:44312/task
        [HttpGet]
        public ActionResult List()
        {
            List<Models.Task> tasks = taskDAO.List();
            if (tasks == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(tasks);
            }
        }

        // https://localhost:44312/task/important
        [HttpGet("important")]
        public ActionResult ListImportant()
        {
            List<Models.Task> tasks = taskDAO.ListImportant();
            if (tasks == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(tasks);
            }
        }

        // https://localhost:44312/task/recurring
        [HttpGet("recurring")]
        public ActionResult ListRecurring()
        {
            List<Models.Task> tasks = taskDAO.ListRecurring();
            if (tasks == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(tasks);
            }
        }

        // https://localhost:44312/task/folder/1
        [HttpGet("folder/{id}")]
        public ActionResult ListByFolderId(int id)
        {
            List<Models.Task> tasks = taskDAO.ListByFolderId(id);
            if (tasks == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(tasks);
            }
        }

        // https://localhost:44312/task
        [HttpPost]
        public IActionResult Create(Models.Task taskToCreate)
        {
            Models.Task createdTask = taskDAO.Create(taskToCreate);
            return Created($"{createdTask.TaskId}", createdTask);
        }

        // https://localhost:44312/task/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (taskDAO.Delete(id))
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        // https://localhost:44312/task/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Models.Task taskToUpdate)
        {
            Models.Task updatedTask = taskDAO.Update(id, taskToUpdate);
            if (updatedTask == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(updatedTask);
            }
        }
    }
}