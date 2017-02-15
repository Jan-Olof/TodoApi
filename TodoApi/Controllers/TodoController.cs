using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    /// <summary>
    /// The Todo controller handles the ToDo list.
    /// </summary>
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        /// <summary>
        /// The costructor for the TodoController.
        /// </summary>
        public TodoController(ITodoRepository todoItems)
        {
            TodoItems = todoItems;
        }

        /// <summary>
        /// Get and set the todo items.
        /// </summary>
        public ITodoRepository TodoItems { get; set; }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Note that the key is a GUID and not an integer.
        ///
        ///     POST /Todo
        ///     {
        ///        "key": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>New Created Todo Item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(TodoItem), 201)]
        [ProducesResponseType(typeof(TodoItem), 400)]
        public IActionResult Create([FromBody, Required] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            TodoItems.Add(item);
            return CreatedAtRoute("GetTodo", new { id = item.Key }, item);
        }

        /// <summary>
        /// Delete a todo item.
        /// </summary>
        /// <param name="id">The item to be deleted.</param>
        /// <returns>Returns a 204 if item is deleted.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            TodoItems.Remove(id);
            return new NoContentResult();
        }

        /// <summary>
        /// Gets all todo items.
        /// </summary>
        /// <returns>Returns a 200 with all items.</returns>
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        /// <summary>
        /// Tests exceptions etc.
        /// </summary>
        [HttpGet("complex/{id}/{name}")]
        public IActionResult GetComplex(string id, string name)
        {
            if (name.Equals("na"))
            {
                throw new System.ArgumentException("The exception message that we have written ourselves.");
            }

            var item = TodoItems.Find(id);
            if (item == null)
            {
                return new BadRequestResult();
            }

            return new ObjectResult(item) { StatusCode = (int?)HttpStatusCode.OK };
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            TodoItems.Update(item);
            return new NoContentResult();
        }
    }
}