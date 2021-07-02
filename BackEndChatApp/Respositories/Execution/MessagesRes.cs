
using Microsoft.EntityFrameworkCore;
using BackEndChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndChatApp.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using BackEndChatApp.Models.Hubs;
using BackEndChatApp.Models.Interfaces;


namespace BackEndChatApp.Respositories.Execution
{
    public class MessagesRes : IMessagesRes
    {
        private const string MESSAGE = "Message";
        private const string CONTENT_MESSAGE = "You have a new message <3";
        private readonly ChatAppRealtimeContext _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        public MessagesRes( ChatAppRealtimeContext context, IHubContext<BroadcastHub,IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        public async Task<IEnumerable<MessageDetails>> GetAllMess(Member mb)
        {
            var messages = from up in _context.UserPeople
                           join me in _context.Members
                           on up.PersonId equals me.UserId
                           join gr in _context.GroupPeople
                           on me.GroupId equals gr.GroupId
                           join sms in _context.SmsMessages
                           on gr.GroupId equals sms.GroupId
                           where up.PersonId == mb.UserId && gr.GroupId == mb.GroupId
                           select new MessageDetails
                           {
                               MessageText = sms.MessageText,
                               Status = sms.Status,
                               SendTime = sms.SendTime,
                               FromId = sms.FromId,
                               Avatar = up.Avatar
                           };
                return await messages.AsNoTracking().ToListAsync();

        }

        public async Task<SmsMessage> SendMess(SmsMessage sms)
        {
            _context.SmsMessages.Add(sms);
            List<int> listMember = await (from gr in _context.GroupPeople
                                    join mb in _context.Members
                                    on gr.GroupId equals mb.GroupId
                                    where gr.GroupId==sms.GroupId && mb.UserId!=sms.FromId
                                    select mb.UserId).ToListAsync<int>();
            foreach (var item in listMember)
            {
                Notification notification = new Notification()
                {
                    NotiType = MESSAGE,
                    ContextText = CONTENT_MESSAGE,
                    FromId = sms.FromId,
                    ToId = item
                };
                _context.Notifications.Add(notification);
            }
                
            
            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.BroadCastMessage();
            }
            catch (DbUpdateException)
            {
                throw;
            }
            
            return sms;
        }
    }
}
