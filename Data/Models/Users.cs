using System;
using System.Collections.Generic;

namespace SeoSoft.Models
{
    public partial class Users
    {
        public Users()
        {
            Project = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastOnline { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<Project> Project { get; set; }
    }
}
