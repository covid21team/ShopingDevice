using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Config
    {
        public Config()
        {
            Configdetail = new HashSet<Configdetail>();
        }

        public string Configname { get; set; }
        public string Decriptionconfigname { get; set; }

        public ICollection<Configdetail> Configdetail { get; set; }
    }
}
