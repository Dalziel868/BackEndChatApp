using BackEndChatApp.Models;
using BackEndChatApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Respositories
{
    public interface IGroupRes
    {
        Task<GroupPerson> CreateGroup(GroupPerson gr);
        Task<GroupPerson> DetailGroup(int idGroup,PersonView pw);
        bool ExistsGroup(int id);
    }
}
