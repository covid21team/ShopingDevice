using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP_Covid21.Models.ApiObject
{
    public class Account
    {
        public string user { get; set; }
        public string pass { get; set; }
    }

    public class AccountLogin
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }
}