using BackEndChatApp.Models;
using BackEndChatApp.Models.ViewModels;
using BackEndChatApp.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Controllers
{

    //Wrong... need revised... This controller is for Manager not clients
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUserPersonRes _res;
        public PersonController(IUserPersonRes res)
        {
            _res = res;
        }

       
        [HttpGet("get-all-people")]
        public async Task<IEnumerable<UserPerson>> GetAllPeople()
        {
            return await _res.GetAllPeople();
        }
        
        [HttpGet("get/{id}")]
        public async Task<ActionResult<UserPerson>> GetPerson(int id)
        {
            return await _res.GetPerson(id);
        }

       
        [HttpPut("update-person/{id}")]
        public async Task<IActionResult> UpdatePerson(int id,UserPerson up)
        {
            if(id!=up.PersonId)
            {
                return BadRequest();
            }
            try
            {
                await _res.UpdatePerson(up);
            }
            catch (DbUpdateException)
            {
                if(!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                
            }
            return NoContent();
        }


        private bool PersonExists(int id)
        {
            return _res.PersonExists(id);
        }

        
        [HttpPost("add-person")]
        public async Task<ActionResult<UserPerson>> CreatePerson(UserPerson up)
        {
            await _res.CreatePerson(up);
            return CreatedAtAction(nameof(GetPerson), new { id = up.PersonId }, up);
           
        }

        [HttpDelete("delete-person")]
        public async Task<IActionResult> DeletePerson(PersonViewModel pw)
        {
            var person = await _res.GetPerson(pw.PersonID);
            if(person==null)
            {
                return NotFound();
            }
            else
            {
               await _res.DeletePerson(person);
                return NoContent();
            }
            
        }
    }
}
