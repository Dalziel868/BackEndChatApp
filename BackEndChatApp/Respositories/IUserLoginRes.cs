using BackEndChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Respositories
{
    public interface IUserLoginRes
    {
        Task<bool> Login(UserLogin login);
    }
}
