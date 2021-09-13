using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBaucuaPlayer
    {
        public WwBaucuaPlayer()
        {
            WwBaucuaChat = new HashSet<WwBaucuaChat>();
            WwBaucuaPlayerdetail = new HashSet<WwBaucuaPlayerdetail>();
        }

        public int Playerid { get; set; }
        public int Roomid { get; set; }
        public string Username { get; set; }
        public bool Ready { get; set; }
        public int? Colorid { get; set; }
        public bool? Dice { get; set; }

        public WwBaucuaColor Color { get; set; }
        public WwBaucuaRoom Room { get; set; }
        public WwBaucuaAccount UsernameNavigation { get; set; }
        public ICollection<WwBaucuaChat> WwBaucuaChat { get; set; }
        public ICollection<WwBaucuaPlayerdetail> WwBaucuaPlayerdetail { get; set; }
    }
}
