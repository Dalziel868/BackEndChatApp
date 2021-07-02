using BackEndChatApp.Models.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Models.Hubs
{
    public class BroadcastHub:Hub<IHubClient>
    {

    }
}
