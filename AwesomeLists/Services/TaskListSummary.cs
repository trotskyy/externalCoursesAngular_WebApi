using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeLists.Services
{
    public class TaskListSummary
    {
        public string TaskListId { get; set; }

        public string Name { get; set; }

        public int Total { get; set; }

        public int ToDoTotal { get; set; }

        public int InProgressTotal { get; set; }

        public int DoneTotal { get; set; }


    }
}
