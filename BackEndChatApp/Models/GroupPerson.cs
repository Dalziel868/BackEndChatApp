using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class GroupPerson
    {
        public GroupPerson()
        {
            Members = new HashSet<Member>();
            SmsMessages = new HashSet<SmsMessage>();
        }

        public int GroupId { get; set; }
        public int? AdminPerson { get; set; }
        public string GroupName { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? GroupType { get; set; }

        public virtual ICollection<Member> Members { get; set; }
        public virtual ICollection<SmsMessage> SmsMessages { get; set; }
    }
}
