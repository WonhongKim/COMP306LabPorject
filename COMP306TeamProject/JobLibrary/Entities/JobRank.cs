using System;
using System.Collections.Generic;

namespace JobLibrary.Entities
{
    public partial class JobRank
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Rank { get; set; }
        public string BestLocations { get; set; }
        public string WorstLocations { get; set; }

        public virtual Job Job { get; set; }
    }
}
