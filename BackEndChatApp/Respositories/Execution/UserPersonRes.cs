using BackEndChatApp.Models;
using BackEndChatApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Respositories.Execution
{
    public class UserPersonRes:IUserPersonRes
    {
        private readonly ChatAppRealtimeContext _context;
        public UserPersonRes(ChatAppRealtimeContext context)
        {
            _context = context;
        }

        public async Task<UserPerson> CreatePerson(UserPerson up)
        {
           
            _context.UserPeople.Add(up);
            try
            {
                await _context.SaveChangesAsync();
                //UserLogin userlogin = new UserLogin()
                //{
                //    PersonId = up.UserLogin.PersonId,
                //    Password = up.UserLogin.Password,
                //    Email = up.UserLogin.Email,
                //    LoginTime = up.UserLogin.LoginTime,
                //    PhoneNumber = up.UserLogin.PhoneNumber

                //};
                //_context.UserLogins.Add(userlogin);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {

                throw;
            }
            return up;
        }

        public async Task DeletePerson(UserPerson up)
        {
            _context.UserPeople.Remove(up);
            await _context.SaveChangesAsync();
        }

      

        public async Task<IEnumerable<Models.UserPerson>> GetAllPeople()
        {
            return await _context.UserPeople.AsNoTracking().ToListAsync();
        }

        public async Task<Models.UserPerson> GetPerson(int id)
        {
            return await _context.UserPeople.FindAsync(id);
        }

        public bool PersonExists(int id)
        {
            return _context.UserPeople.Any(p => p.PersonId == id);
        }

        public async Task UpdatePerson(UserPerson up)
        {
            _context.UserPeople.Update(up);
            await _context.SaveChangesAsync();
        }
    }
}
