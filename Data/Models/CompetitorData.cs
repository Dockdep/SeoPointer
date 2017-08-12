using System;
using System.Collections.Generic;

namespace SeoSoft.Models
{
    public partial class CompetitorData
    {
        public int Id { get; set; }
        public int CompetitorId { get; set; }
        public int CompetitorItemId { get; set; }
        public int CompetitorFieldId { get; set; }
        public string Data { get; set; }

        public virtual CompetitorField CompetitorField { get; set; }
        public virtual Competitor Competitor { get; set; }
        public virtual CompetitorItem CompetitorItem { get; set; }
    }
}
