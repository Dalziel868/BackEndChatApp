using BackEndChatApp.Models;
using BackEndChatApp.Models.ViewModels;
using BackEndChatApp.Respositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRes _groupRes;
        public GroupsController(IGroupRes groupRes)
        {
            _groupRes = groupRes;
        }

        [HttpGet("go-to-group/{idGroup}")]
        [AllowAnonymous]
        public async Task<ActionResult<GroupPerson>> GetGroup(int idGroup,[FromBody]PersonView pw)
        {
            var group = await _groupRes.DetailGroup(idGroup,pw);
            if(group==null)
            {
                return NotFound();
            }
            return group;
        }
        [HttpPost("add-group")]
        public async Task<ActionResult<GroupPerson>> CreateGroup(GroupPerson gr)
        {
            var addGroup = await _groupRes.CreateGroup(gr);
            if(addGroup==null)
            {
                return Conflict();
            }
            return CreatedAtAction(nameof(GetGroup),new { idGroup = gr.GroupId},gr);

        }
    }
}
