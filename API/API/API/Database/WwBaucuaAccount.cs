using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBaucuaAccount
    {
        public WwBaucuaAccount()
        {
            WwBaucuaPlayer = new HashSet<WwBaucuaPlayer>();
            WwBaucuaRoom = new HashSet<WwBaucuaRoom>();
        }

        public string Accountname { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public double Money { get; set; }
        public bool? AcStatus { get; set; }

        public ICollection<WwBaucuaPlayer> WwBaucuaPlayer { get; set; }
        public ICollection<WwBaucuaRoom> WwBaucuaRoom { get; set; }
    }
}
