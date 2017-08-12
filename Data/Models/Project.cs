using System;
using System.Collections.Generic;

namespace SeoSoft.Models
{
    public partial class Project
    {
        public Project()
        {
            Category = new HashSet<Category>();
            Competitor = new HashSet<Competitor>();
            Item = new HashSet<Item>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Contacts { get; set; }
        public string KeyName { get; set; }
        public string EmailList { get; set; }
        public int? Status { get; set; }
        public int UserId { get; set; }
        public string Info { get; set; }

        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<Competitor> Competitor { get; set; }
        public virtual ICollection<Item> Item { get; set; }
        public virtual Users User { get; set; }
    }
}
