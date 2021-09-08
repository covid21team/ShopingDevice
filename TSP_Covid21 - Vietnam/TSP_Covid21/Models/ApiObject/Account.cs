using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TSP_Covid21.Models.ApiObject
{
    public class Account
    {
        public string user { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public bool ? sex { get; set; }
    }

    public class AccountLogin
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }

    public class Message
    {
        private int status;
        private string notification;

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }

        public Message(int status, string notification)
        {
            Status = status;
            Notification = notification;
        }
    }
}