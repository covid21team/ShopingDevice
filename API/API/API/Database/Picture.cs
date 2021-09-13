using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Picture
    {
        public Picture()
        {
            PicProduct = new HashSet<PicProduct>();
        }

        public int Pictureid { get; set; }
        public string Path { get; set; }
        public string Link { get; set; }

        public ICollection<PicProduct> PicProduct { get; set; }
    }
}
