using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobInfoAPI.Models;
using JobInfoAPI.Servies;
using JobLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobInfoAPI.Controllers
{
    [Route("api/JobInfo")]
    public class JobRankController : ControllerBase
    {

        private IJobInfoRepository _JobInfoRepository;
        private readonly IMapper _mapper;

        public JobRankController(IJobInfoRepository cityInfoRepository, IMapper mapper)
        {
            _JobInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        [HttpGet("{JobId}/jobrank")]
        public async Task<ActionResult<JobRank>> GetJobRank(int JobId)
        {
            if (!(await _JobInfoRepository.JobExists(JobId)))
            {
                return NotFound();
            }

            var JobRankForView = await _JobInfoRepository.GetJobRank(JobId);
            var results = _mapper.Map<JobRankDto>(JobRankForView);

            return Ok(results);
        }    

        [HttpPost("{JobId}/jobrank")]
        public async Task<ActionResult<JobRankDto>> CreateJobRank(int JobId, [FromBody]JobRank jobrank)
        {
            if (jobrank == null)  return BadRequest();
            
            if (jobrank.BestLocations == jobrank.WorstLocations)
            {
                ModelState.AddModelError("Location", "Best and Worst cannot be same");
            }
            var checker = _JobInfoRepository.GetJobRank(JobId);
        
            if (!ModelState.IsValid) return BadRequest(ModelState);         
            
            if (! await _JobInfoRepository.JobExists(JobId)) return NotFound();

            if (checker != null)
            {
                ModelState.AddModelError("Error", "Rank data is already exist");
            }

            var finaljobrank = _mapper.Map<JobRank>(jobrank);

            await _JobInfoRepository.AddJobRankForJob(JobId, finaljobrank);

            if (! await _JobInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            var createdPointOfInterestToReturn = _mapper.Map<JobRankDto>(finaljobrank);
            return CreatedAtAction("GetPointOfInterest", new { JobId = JobId, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);
        }

        [HttpPut("{JobId}/jobrank/{id}")]
        public async Task<ActionResult> UpdateJobRank(int JobId, int id, [FromBody] JobRankForUpdate jobrank)
        {
            if (jobrank == null) return BadRequest();

            if (jobrank.BestLocations == jobrank.WorstLocations)
            {
                ModelState.AddModelError("Location", "Best and Worst cannot be same");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _JobInfoRepository.JobExists(JobId)) return NotFound();

            JobRank oldPointOfJobRankEntity = await _JobInfoRepository.GetJobRankForJob(JobId, id);

            if (oldPointOfJobRankEntity == null) return NotFound();

            _mapper.Map(jobrank, oldPointOfJobRankEntity);


            if (!await _JobInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{JobId}/jobrank/{id}")]
        public async Task<ActionResult> DeleteJobRankt(int JobId, int id)
        {
            if (!await _JobInfoRepository.JobExists(JobId)) return NotFound();

            JobRank jobrankfordelete = await _JobInfoRepository.GetJobRankForJob(JobId, id);
            if (jobrankfordelete == null) return NotFound();

            _JobInfoRepository.DeleteJobRank(jobrankfordelete);

            if (!await _JobInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
