using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Repositories;

namespace TaskFlow.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public JobController(IJobRepository jobRepository, IUserProfileRepository userProfileRepository)
        {
            _jobRepository = jobRepository;
            _userProfileRepository = userProfileRepository;
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
            job.CreateDate = DateTime.Now;

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

        [HttpGet("GetJobByIdWithDetails{id}")]
        public IActionResult GetJobByIdWithDetails(int id)
        {
            var job = _jobRepository.GetJobByIdWithDetails(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpGet("GetAllJobsByCustomerId{id}")]
        public IActionResult GetAllJobsByCustomerId(int id)
        {
            var job = _jobRepository.GetAllJobsByCustomerId(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }


        [HttpGet("GetJobsByWorkDayUser")]
        public IActionResult GetJobsByWorkDayUser()
        {
            var currentUserProfile = GetCurrentUserProfile();
            int userId = currentUserProfile.Id;
            var job = _jobRepository.GetJobsByWorkDayUser(userId);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }



        [HttpPut("ComepleteJob{id}")]
        public IActionResult ComepleteJob(int id, DateTime complete)
        {
             complete = DateTime.Now;

            {
                _jobRepository.ComepleteJob(id, complete);
                return NoContent();
            }

        }

        [HttpGet("GetAllUncompleteJobs")]
        public IActionResult GetAllUncompleteJobs()
        {
            return Ok(_jobRepository.GetAllUncompleteJobs());
        }


        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }



    }

}
