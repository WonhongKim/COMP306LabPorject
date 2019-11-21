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
    [Route("api/[controller]")]
    public class JobInfoController : ControllerBase
    {
        private IJobInfoRepository _JobInfoRepository;
        private readonly IMapper _mapper;

        public JobInfoController(IJobInfoRepository cityInfoRepository, IMapper mapper)
        {
            _JobInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }

        public async Task<ActionResult<Job>> GetJobs()
        {
            var JobEntities = await _JobInfoRepository.GetJobs();

            var results = _mapper.Map<IEnumerable<JobDto>>(JobEntities);

            return Ok(results);
        }

        [HttpGet("{JobId}")]
        public async Task<ActionResult<Job>> GetJobById(int JobId)
        {
            if (!(await _JobInfoRepository.JobExists(JobId)))
            {
                return NotFound();
            }

            var Job = await _JobInfoRepository.GetJobById(JobId);
            var results = _mapper.Map<JobDto>(Job);

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob([FromBody]JobForCreateDto NewJob)
        {
           if (NewJob == null) return BadRequest();
           
            if (NewJob.JobTitle == NewJob.Discription)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }           

            if (!ModelState.IsValid) return BadRequest(ModelState);            

            var finalNewJob = _mapper.Map<Job>(NewJob);

            _JobInfoRepository.AddNewJob(finalNewJob);

            if (!await _JobInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdNewJobToReturn = _mapper.Map<Job>(finalNewJob);

            return CreatedAtAction("GetNewJob", new { JobId = finalNewJob.JobId }, createdNewJobToReturn);
        }     


    }
}
