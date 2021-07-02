using BackEndChatApp.Models;
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

        public async Task<Models.UserPerson> CreatePerson(Models.UserPerson up)
        {
            _context.UserPeople.Add(up);
            await _context.SaveChangesAsync();
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
