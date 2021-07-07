using BackEndChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Respositories.Execution
{
    public class UserLoginRes : IUserLoginRes
    {
        private readonly ChatAppRealtimeContext _context;
       public UserLoginRes(ChatAppRealtimeContext context)
        {
            _context = context;
        }
        public async Task<bool> Login(UserLogin login)
        {
            var checkEmail = await _context.UserLogins.FirstOrDefaultAsync(s => s.Email.Equals(login.Email));

            if(checkEmail==null || !checkEmail.Password.Equals(login.Password))
            {
                return false;
            }
            return true;
        }
    }
}
