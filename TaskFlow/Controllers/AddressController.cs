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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_addressRepository.GetAll());
        }

        

        [HttpPost]
        public IActionResult Post(CustomerAddress address)
        {
            _addressRepository.Add(address);
            return CreatedAtAction(nameof(Get), new { id = address.Id }, address);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _addressRepository.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, CustomerAddress address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _addressRepository.Update(address);
            return NoContent();
        }


        




    }
}
