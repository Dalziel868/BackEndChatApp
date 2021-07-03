using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndChatApp.Models.ViewModels
{
    public class MessageDetailsViewModel
    {

        public string MessageText { get; set; }
        public string Status { get; set; }
        public DateTime? SendTime { get; set; }
        public int? FromId { get; set; }
        public byte[] Avatar { get; set; }

    }
}
