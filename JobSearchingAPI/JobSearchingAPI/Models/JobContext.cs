using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobSearchingAPI.Models;

namespace JobSearchingAPI.Models
{
    public class JobContext :DbContext
    {
        public JobContext(DbContextOptions<JobContext> options) : base(options) { }

        public DbSet<Job> jobs { get; set; }
    }
}
