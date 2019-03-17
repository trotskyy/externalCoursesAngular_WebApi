using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeLists.Services.User
{
    public sealed class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
