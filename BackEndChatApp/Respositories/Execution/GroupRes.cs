using BackEndChatApp.Models;
using BackEndChatApp.Models.Hubs;
using BackEndChatApp.Models.Interfaces;
using BackEndChatApp.Models.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Respositories.Execution
{
    public class GroupRes:IGroupRes
    {
        private const string SYSTEM = "System";
        private readonly ChatAppRealtimeContext _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        public GroupRes (ChatAppRealtimeContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<GroupPerson> CreateGroup(GroupPerson gr)
        {
            _context.GroupPeople.Add(gr);

            if (gr.GroupType==false)
            {
                Notification notification = new Notification()
                {
                    NotiType = SYSTEM,
                    ContextText = $"You just created a group named {gr.GroupName}",
                    ToId=gr.AdminPerson
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

                if(ExistsGroup(gr.GroupId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return gr;
        }

        public async Task<GroupPerson> DetailGroup(int idGroup, PersonViewModel pw)
        {
            var detail = await _context.Members.FirstOrDefaultAsync(m => m.GroupId == idGroup && m.UserId == pw.PersonID);
            if(detail==null)
            {
                return null;
            }
            return await _context.GroupPeople.FindAsync(idGroup);
        }

        public bool ExistsGroup(int id)
        {
            return _context.GroupPeople.Any(g => g.GroupId == id);
        }
    }
}
