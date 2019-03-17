using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeLists.Services
{
    public class Task
    {
        public string TaskId { get; set; }

        public string TaskListId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
