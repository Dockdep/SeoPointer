using System;
using System.Collections.Generic;

namespace SeoSoft.Models
{
    public partial class CompetitorField
    {
        public CompetitorField()
        {
            CompetitorData = new HashSet<CompetitorData>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CompetitorId { get; set; }
        public string Regexp { get; set; }
        public string XPath { get; set; }

        public virtual ICollection<CompetitorData> CompetitorData { get; set; }
        public virtual Competitor Competitor { get; set; }
    }
}
