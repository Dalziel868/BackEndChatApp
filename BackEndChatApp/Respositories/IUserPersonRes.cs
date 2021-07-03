using BackEndChatApp.Models;
using BackEndChatApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Respositories
{
    public interface IUserPersonRes
    {
        Task<IEnumerable<UserPerson>> GetAllPeople();
        Task<UserPerson> GetPerson(int id);
        Task<UserPerson> CreatePerson(UserPerson up);
       
        Task UpdatePerson(UserPerson up);
        Task DeletePerson(UserPerson up);

        bool PersonExists(int id);
    }
}
