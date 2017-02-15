using System.Collections.Generic;

namespace TodoApi.Models
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);

        TodoItem Find(string key);

        IEnumerable<TodoItem> GetAll();

        TodoItem Remove(string key);

        void Update(TodoItem item);
    }
}