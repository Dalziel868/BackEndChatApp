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
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesRes _messageRES;
        public MessagesController(IMessagesRes messages)
        {
            _messageRES = messages;
        }

        [HttpGet("read-message")]
        [AllowAnonymous]
        public async Task<IEnumerable<MessageDetails>> GetMessage([FromBody]Member mb)
        {
           return await _messageRES.GetAllMess(mb);
           
        }
        [HttpPost("send-message")]
        public async Task<ActionResult<SmsMessage>> SendMessage(SmsMessage sm)
        {
            return await _messageRES.SendMess(sm);
        }
    }
}
