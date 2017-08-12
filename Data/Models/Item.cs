using System;
using System.Collections.Generic;

namespace SeoSoft.Models
{
    public partial class Item
    {
        public Item()
        {
            CompetitorItem = new HashSet<CompetitorItem>();
        }

        public int Id { get; set; }
        public string ItemName { get; set; }
        public double? Price { get; set; }
        public int CategoryId { get; set; }
        public int? Status { get; set; }
        public string Url { get; set; }
        public int ProjectId { get; set; }
        public string GoogleUrl { get; set; }

        public virtual ICollection<CompetitorItem> CompetitorItem { get; set; }
        public virtual Category Category { get; set; }
        public virtual Project Project { get; set; }
    }
}
