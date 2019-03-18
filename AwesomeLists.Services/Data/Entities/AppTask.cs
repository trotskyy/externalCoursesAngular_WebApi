using System;

namespace AwesomeLists.Data.Entities
{
    public class AppTask
    {
        public int Id { get; set; }

        public int TaskListId { get; set; }
        public TaskList TaskList { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
