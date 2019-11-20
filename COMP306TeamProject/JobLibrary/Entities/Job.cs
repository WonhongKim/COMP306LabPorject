using System;
using System.Collections.Generic;

namespace JobLibrary.Entities
{
    public partial class Job
    {
        public Job()
        {
            JobRank = new HashSet<JobRank>();
        }

        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string Discription { get; set; }
        public string JobCategory { get; set; }

        public virtual ICollection<JobRank> JobRank { get; set; }
    }
}
