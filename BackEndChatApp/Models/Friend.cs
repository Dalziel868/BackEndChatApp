using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class Friend
    {
        public int FriendId { get; set; }
        public int PersonId { get; set; }

        public virtual UserPerson Person { get; set; }
    }
}
