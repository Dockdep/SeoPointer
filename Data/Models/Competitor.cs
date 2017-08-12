using System;
using System.Collections.Generic;

namespace SeoSoft.Models
{
    public partial class Competitor
    {
        public Competitor()
        {
            CompetitorData = new HashSet<CompetitorData>();
            CompetitorField = new HashSet<CompetitorField>();
            CompetitorItem = new HashSet<CompetitorItem>();
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string TestUrl { get; set; }
        public int ProjectId { get; set; }
        public string Email { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<CompetitorData> CompetitorData { get; set; }
        public virtual ICollection<CompetitorField> CompetitorField { get; set; }
        public virtual ICollection<CompetitorItem> CompetitorItem { get; set; }
        public virtual Project Project { get; set; }
    }
}
