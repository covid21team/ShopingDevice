using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBaucuaColor
    {
        public WwBaucuaColor()
        {
            WwBaucuaPlayer = new HashSet<WwBaucuaPlayer>();
        }

        public int Colorid { get; set; }
        public string Colorname { get; set; }

        public ICollection<WwBaucuaPlayer> WwBaucuaPlayer { get; set; }
    }
}
