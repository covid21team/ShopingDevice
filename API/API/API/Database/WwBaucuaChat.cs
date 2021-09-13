using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBaucuaChat
    {
        public int Chatid { get; set; }
        public int? Playerid { get; set; }
        public string Textchat { get; set; }
        public int Numberviewers { get; set; }

        public WwBaucuaPlayer Player { get; set; }
    }
}
