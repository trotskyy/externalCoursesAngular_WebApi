using System.Collections.Generic;

namespace AwesomeLists.Data.Entities
{
    public class TaskList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<AppTask> Tasks { get; set; }
    }
}
