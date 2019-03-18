using System.Collections.Generic;

namespace AwesomeLists.Data.Entities
{
    public sealed class User
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<TaskList> TaskLists { get; set; }
    }
}
