using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Models.Interfaces
{
    public interface IHubClient
    {
        Task BroadCastMessage();
    }
}
