using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeLists.Services
{
    public class TaskList
    {
        public string TaskListId { get; set; }

        public string Name { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
