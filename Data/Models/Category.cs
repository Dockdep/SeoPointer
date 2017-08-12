using System;
using System.Collections.Generic;

namespace SeoSoft.Models
{
    public partial class Category
    {
        public Category()
        {
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Item> Item { get; set; }
        public virtual Project Project { get; set; }
    }
}
