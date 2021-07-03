using BackEndChatApp.Models;
using BackEndChatApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Respositories
{
    public interface IMessagesRes
    {
        Task<IEnumerable<MessageDetailsViewModel>> GetAllMess(Member mb);
        Task<SmsMessage> SendMess(SmsMessage sms);
    }
}
