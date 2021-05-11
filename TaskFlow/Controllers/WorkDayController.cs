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
    public class WorkDayController : ControllerBase
    {

        private readonly IWorkDayRepository _workDayRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public WorkDayController(IWorkDayRepository workDayRepository, IUserProfileRepository userProfileRepository)
        {
            _workDayRepository = workDayRepository;
            _userProfileRepository = userProfileRepository;
        }



        [HttpPost]
        public IActionResult Post(WorkDay workDay)
        {
            var currentUserProfile = GetCurrentUserProfile();
            workDay.UserProfileId = currentUserProfile.Id;

            _workDayRepository.AddWorkDay(workDay);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _workDayRepository.DeleteWorkDay(id);
            return NoContent();
        }



        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }



    }
}
