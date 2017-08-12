using System;
using System.Collections.Generic;

namespace SeoSoft.Models
{
    public partial class CompetitorItem
    {
        public CompetitorItem()
        {
            CompetitorData = new HashSet<CompetitorData>();
        }

        public int Id { get; set; }
        public int ItemId { get; set; }
        public int CompetitorId { get; set; }
        public string Url { get; set; }
        public int? Status { get; set; }
        public string GoogleLink { get; set; }
        public string LinkStatus { get; set; }

        public virtual ICollection<CompetitorData> CompetitorData { get; set; }
        public virtual Competitor Competitor { get; set; }
        public virtual Item Item { get; set; }
    }
}
