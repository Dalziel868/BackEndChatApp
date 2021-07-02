using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class UserPerson
    {
        public UserPerson()
        {
            Friends = new HashSet<Friend>();
            Members = new HashSet<Member>();
        }

        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string Introduce { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Gender { get; set; }
        public byte[] Avatar { get; set; }
        public byte[] CoverImage { get; set; }
        public int? Status { get; set; }

        public virtual UserLogin UserLogin { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
