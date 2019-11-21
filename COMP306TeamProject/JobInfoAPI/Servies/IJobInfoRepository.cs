using JobLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobInfoAPI.Servies
{
    public interface IJobInfoRepository
    {
        Task<bool> JobExists(int JobId);

        Task<IEnumerable<Job>> GetJobs();
        Task<Job> GetJobById(int jobId);
        Task<JobRank> GetJobRank(int jobId);
        void AddNewJob(Job newJob);
        Task AddJobRankForJob(int JobId, JobRank jobrank);
        Task<JobRank> GetJobRankForJob(int JobId, int JobRankId);
        void DeleteJobRank(JobRank jobrank);
        Task<bool> Save();

    }
}
