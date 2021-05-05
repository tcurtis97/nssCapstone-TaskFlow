using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Repositories;

namespace TaskFlow.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        public JobController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_jobRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var job = _jobRepository.GetById(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpPost]
        public IActionResult Post(Job job)
        {
            _jobRepository.Add(job);
            return CreatedAtAction(nameof(Get), new { id = job.Id }, job);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _jobRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            _jobRepository.Update(job);
            return NoContent();
        }








    }

}
