using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class Member
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }

        public virtual GroupPerson Group { get; set; }
        public virtual UserPerson User { get; set; }
    }
}
