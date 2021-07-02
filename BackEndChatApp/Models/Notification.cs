using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class Notification
    {
        public int NotifiId { get; set; }
        public string NotiType { get; set; }
        public string ContextText { get; set; }
        public int? FromId { get; set; }
        public int? ToId { get; set; }
    }
}
