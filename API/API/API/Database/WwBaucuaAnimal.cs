using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class WwBaucuaAnimal
    {
        public WwBaucuaAnimal()
        {
            WwBaucuaPlayerdetail = new HashSet<WwBaucuaPlayerdetail>();
        }

        public int Animalid { get; set; }
        public string Animalname { get; set; }

        public ICollection<WwBaucuaPlayerdetail> WwBaucuaPlayerdetail { get; set; }
    }
}
