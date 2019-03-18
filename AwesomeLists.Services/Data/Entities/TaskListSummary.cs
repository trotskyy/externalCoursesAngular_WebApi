using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeLists.Data.Entities
{
    public sealed class TaskListSummary
    {
        public int TaskListId { get; set; }
        public string ListName { get; set; }
        public int ToDoCount { get; set; }
        public int InProgressCount { get; set; }
        public int DoneCount { get; set; }
    }
}
