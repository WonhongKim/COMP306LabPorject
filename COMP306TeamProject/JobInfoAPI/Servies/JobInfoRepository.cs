using JobLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobInfoAPI.Servies
{
    public class JobInfoRepository : IJobInfoRepository
    {
        private JobDBContext _context;
        public JobInfoRepository(JobDBContext context)
        {
            _context = context;
        }

        public async Task<bool> JobExists(int JobId)
        {
            return await _context.Job.AnyAsync<Job>(c => c.JobId == JobId);
        }

        public async Task<IEnumerable<Job>> GetJobs()
        {
            var result = _context.Job.OrderBy(c => c.JobTitle);
            return await result.ToListAsync();
        }
        public async Task<Job>GetJobById(int JobId)
        {
            IQueryable<Job> result;
            result = _context.Job.Where(c => c.JobId == JobId);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<JobRank> GetJobRank(int JobId)
        {
            IQueryable<JobRank> result = _context.JobRank.Where(p => p.JobId == JobId);
            return await result.FirstOrDefaultAsync();
        }

        public void AddNewJob(Job newJob)
        {
            _context.Job.Add(newJob);
        }

        public async Task AddJobRankForJob(int JobId, JobRank jobrank)
        {
            var Job = await GetJobById(JobId);
            Job.JobRank.Add(jobrank);
        }

        public async Task<JobRank> GetJobRankForJob(int JobId, int JobRankId)
        {
            IQueryable<JobRank> result = _context.JobRank.Where(p => p.JobId == JobId && p.Id == JobRankId);
            return await result.FirstOrDefaultAsync();
        }

        public void DeleteJobRank(JobRank jobrank)
        {
            _context.JobRank.Remove(jobrank);
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }

}

