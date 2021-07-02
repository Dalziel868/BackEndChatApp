using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class SmsMessage
    {
        public Guid MessId { get; set; }
        public int GroupId { get; set; }
        public string MessageText { get; set; }
        public string Status { get; set; }
        public DateTime? SendTime { get; set; }
        public int? FromId { get; set; }

        public virtual GroupPerson Group { get; set; }
    }
}
