using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            Ipconfigs = new HashSet<Ipconfig>();
        }

        public int PersonId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? LoginTime { get; set; }
        public string PhoneNumber { get; set; }

        public virtual UserPerson Person { get; set; }
        public virtual ICollection<Ipconfig> Ipconfigs { get; set; }
    }
}
