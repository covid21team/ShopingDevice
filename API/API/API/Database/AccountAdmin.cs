using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class AccountAdmin
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public bool? Statusaccount { get; set; }
    }
}
