using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobSearchingAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobSearchingAPI.Controllers
{
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {

        private readonly JobContext _context;

        public JobController(JobContext context)
        {
            _context = context;
            if (_context.jobs.Count() == 0)
            {
                _context.jobs.Add(new Job {  skillSet = "level 0", jobTitle ="waiter", jobSalary = "16$ per Hr", company ="popoys", WorkHours ="20 hr per week", location ="xxx" , jobDescription ="yyy" });
                _context.jobs.Add(new Job { skillSet ="level c", jobTitle ="security", jobSalary = "17$ per Hr", company ="BMO", WorkHours ="21 hr per week", location ="xxx" , jobDescription ="yyy" });
                _context.jobs.Add(new Job {  skillSet ="level c", jobTitle ="cleck", jobSalary = "18$ per Hr", company ="BMO", WorkHours ="15 hr per week", location ="xxx" , jobDescription ="yyy"});
                _context.jobs.Add(new Job {  skillSet ="level 0", jobTitle ="waiter", jobSalary = "16$ per Hr", company ="jack", WorkHours ="19 hr per week", location ="xxx" , jobDescription ="yyy" });
                _context.SaveChangesAsync();
            }

        }
        [HttpGet]
        //[HttpGet("api/items.{format}"),FormatFilter]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob()
        {
            return await _context.jobs.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobById(int JobId)
        {
            var jobvar = await _context.jobs.FindAsync(JobId);
            if (jobvar == null) return NotFound();
            return jobvar;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            _context.jobs.Add(job);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJobById), new { id = job.JobId }, job);

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutJob(int JobId, Job job)
        {

            if (JobId != job.JobId) return BadRequest();
            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJob(int JobId)
        {
            var jobvar = await _context.jobs.FindAsync(JobId);
            if (jobvar == null) return NotFound();
            _context.jobs.Remove(jobvar);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /*     // GET: api/<controller>
             [HttpGet]
             public IEnumerable<string> Get()
             {
                 return new string[] { "value1", "value2" };
             }

             // GET api/<controller>/5
             [HttpGet("{id}")]
             public string Get(int id)
             {
                 return "value";
             }

             // POST api/<controller>
             [HttpPost]
             public void Post([FromBody]string value)
             {
             }

             // PUT api/<controller>/5
             [HttpPut("{id}")]
             public void Put(int id, [FromBody]string value)
             {
             }

             // DELETE api/<controller>/5
             [HttpDelete("{id}")]
             public void Delete(int id)
             {
             }*/
    }
}
