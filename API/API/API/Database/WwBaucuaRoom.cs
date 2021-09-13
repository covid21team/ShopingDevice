using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBaucuaRoom
    {
        public WwBaucuaRoom()
        {
            WwBaucuaPlayer = new HashSet<WwBaucuaPlayer>();
        }

        public int Roomid { get; set; }
        public int? Amountperson { get; set; }
        public bool? Roomstatus { get; set; }
        public double Minbets { get; set; }
        public string Owner { get; set; }
        public string Pass { get; set; }
        public int? Dice1 { get; set; }
        public int? Dice2 { get; set; }
        public int? Dice3 { get; set; }

        public WwBaucuaAccount OwnerNavigation { get; set; }
        public ICollection<WwBaucuaPlayer> WwBaucuaPlayer { get; set; }
    }
}
