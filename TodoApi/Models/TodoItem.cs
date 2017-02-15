using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [DefaultValue(false)]
        public bool IsComplete { get; set; }

        public string Key { get; set; }

        [Required]
        public string Name { get; set; }
    }
}