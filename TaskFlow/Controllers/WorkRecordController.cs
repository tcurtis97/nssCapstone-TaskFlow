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
    [Route("api/[controller]")]
    [ApiController]
    public class WorkRecordController : ControllerBase
    {
        private readonly IWorkRecordRepository _workRecordRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public  WorkRecordController(IWorkRecordRepository  workRecordRepository, IUserProfileRepository userProfileRepository)
        {
            _workRecordRepository =  workRecordRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_workRecordRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var  workRecord = _workRecordRepository.GetById(id);
            if ( workRecord == null)
            {
                return NotFound();
            }
            return Ok( workRecord);
        }

        [HttpPost]
        public IActionResult Post(WorkRecord workRecord)
        {
            var currentUserProfile = GetCurrentUserProfile();

             workRecord.UserProfileId = currentUserProfile.Id;
             workRecord.CreateDate = DateTime.Now;

            _workRecordRepository.Add( workRecord);
            return CreatedAtAction(nameof(Get), new { id =  workRecord.Id },  workRecord);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _workRecordRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, WorkRecord workRecord)
        {
            if (id !=  workRecord.Id)
            {
                return BadRequest();
            }

            _workRecordRepository.Update( workRecord);
            return NoContent();
        }

        [HttpGet("GetAllWorkRecordsByJobId{id}")]
        public IActionResult GetAllWorkRecordsByJobId(int id)
        {
            var  workRecord = _workRecordRepository.GetAllWorkRecordsByJobId(id);
            if ( workRecord == null)
            {
                return NotFound();
            }
            return Ok( workRecord);
        }


        // Retrieves the current user object by using the provided firebaseId
        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }


    }
}
