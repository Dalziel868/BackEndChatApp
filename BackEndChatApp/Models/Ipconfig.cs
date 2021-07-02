using System;
using System.Collections.Generic;

#nullable disable

namespace BackEndChatApp.Models
{
    public partial class Ipconfig
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public int? PersonId { get; set; }

        public virtual UserLogin Person { get; set; }
    }
}
