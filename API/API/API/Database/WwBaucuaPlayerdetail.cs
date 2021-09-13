using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBaucuaPlayerdetail
    {
        public int Playerid { get; set; }
        public int Animalid { get; set; }
        public double Bets { get; set; }

        public WwBaucuaAnimal Animal { get; set; }
        public WwBaucuaPlayer Player { get; set; }
    }
}
