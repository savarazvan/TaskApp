using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public List<TaskItem> Tasks { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
