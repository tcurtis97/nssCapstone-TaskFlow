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
   
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public NoteController(INoteRepository noteRepository, IUserProfileRepository userProfileRepository)
        {
            _noteRepository = noteRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_noteRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var note = _noteRepository.GetById(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }



            [HttpPost]
        public IActionResult Post(Note note)
        {
            var currentUserProfile = GetCurrentUserProfile();

            note.UserProfileId = currentUserProfile.Id;
            note.CreateDate = DateTime.Now;

            _noteRepository.Add(note);
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _noteRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _noteRepository.Update(note);
            return NoContent();
        }

        [HttpGet("GetAllNotesByJobId{id}")]
        public IActionResult GetAllNotesByJobId(int id)
        {
            var note = _noteRepository.GetAllNotesByJobId(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }




        // Retrieves the current user object by using the provided firebaseId
        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }


    }
}
