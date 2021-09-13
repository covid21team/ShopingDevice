using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Object
{
    // Dùng vào input
    public class Account
    {
        public string user { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string date { get; set; }
        public bool? sex { get; set; }
    }

    public class AccountLogin
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }

    // Dùng vào input
    public class AccountSignUp
    {
        public string user { get; set; }
        public string fullname { get; set; }
        public string pass { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
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

    public class MessageLogin
    {
        private int status;
        private string notification;
        private string fullName;
        private string token;

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Token { get => token; set => token = value; }

        public MessageLogin(int status, string notification, string fullname, string token)
        {
            Status = status;
            Notification = notification;
            FullName = fullname;
            Token = token;
        }
    }

    public class MessageGetAccount
    {
        private int status;
        private string notification;
        private Object.Account data;

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public Account Data { get => data; set => data = value; }

        public MessageGetAccount(Object.Account data, int status, string notification)
        {
            Status = status;
            Notification = notification;
            Data = data;
        }
    }

    public class MessageGetAllSeenProduct
    {
        private int status;
        private string notification;
        private IEnumerable<Object.Product> data;

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<Object.Product> Data { get => data; set => data = value; }

        public MessageGetAllSeenProduct(IEnumerable<Object.Product> data, int status, string notification)
        {
            Status = status;
            Notification = notification;
            Data = data;
        }
    }
}