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
        public class CustomerController : ControllerBase
        {
            private readonly ICustomerRepository _customerRepository;
            public CustomerController(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            [HttpGet]
            public IActionResult Get()
            {
                return Ok(_customerRepository.GetAll());
            }

            [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                var customer = _customerRepository.GetById(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }

            [HttpPost]
            public IActionResult Post(Customer customer)
            {
                _customerRepository.Add(customer);
                return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                _customerRepository.Delete(id);
                return NoContent();
            }

            [HttpPut("{id}")]
            public IActionResult Put(int id, Customer customer)
            {
                if (id != customer.Id)
                {
                    return BadRequest();
                }

                _customerRepository.Update(customer);
                return NoContent();
            }


        [HttpGet("search")]
        public IActionResult Search(string q, bool sortDesc)
        {
            return Ok(_customerRepository.Search(q, sortDesc));
        }


        [HttpGet("GetCustomerByIdWithAddressWithJob{id}")]
        public IActionResult GetCustomerByIdWithAddressWithJob(int id)
        {
            var customer = _customerRepository.GetCustomerByIdWithAddressWithJob(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }


    
    
    }
    

}
