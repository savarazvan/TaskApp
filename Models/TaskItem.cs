using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Models
{
    public class TaskItem
    {
        public TaskItem() { }
        public TaskItem(string title, int categoryId)
        {
            Title = title;
            CategoryId = categoryId;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
