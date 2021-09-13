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
        public string user { get; set; }
        public string pass { get; set; }
    }

    // Dùng vào input
    public class AccountSignUp
    {
        public string user { get; set; }
        public string fullname { get; set; }
        public string pass { get; set; }
        public string phonenumber { get; set; }
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

    public class MessageGetCart
    {
        private int status;
        private string notification;
        private IEnumerable<Object.ProductOfCart> cart;

        public MessageGetCart(IEnumerable<ProductOfCart> cart, int status, string notification)
        {
            this.status = status;
            this.notification = notification;
            this.cart = cart;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<ProductOfCart> Cart { get => cart; set => cart = value; }
    }

    public class ProductOfCart
    {
        public int productId { get; set; }
        public string pic { get; set; }
        public string productName { get; set; }
        public bool ? productStatus { get; set; }
        public int ? amount { get; set; }
        public int ? price { get; set; }
    }

    public class MessageOfBills
    {
        private int status;
        private string notification;
        private IEnumerable<Object.Bill> bills;

        public MessageOfBills(IEnumerable<Bill> bills, int status, string notification)
        {
            this.status = status;
            this.notification = notification;
            this.bills = bills;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public IEnumerable<Bill> Bills { get => bills; set => bills = value; }
    }

    public class Bill
    {
        public int billId { get; set; }
        public long ? totalBill { get; set; }
        public DateTime ? date { get; set; }
        public int ? billStatus { get; set; }
        public IEnumerable<string> products { get; set; }
    }

    public class MessageOfBillDetail
    {
        private int status;
        private string notification;
        private BillDetail billDetail;

        public MessageOfBillDetail(BillDetail billDetail,int status, string notification)
        {
            this.status = status;
            this.notification = notification;
            this.billDetail = billDetail;
        }

        public int Status { get => status; set => status = value; }
        public string Notification { get => notification; set => notification = value; }
        public BillDetail BillDetail { get => billDetail; set => billDetail = value; }
    }

    public class BillDetail
    {
        public string city { get; set; }
        public string district { get; set; }
        public string ward { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int? billStatus { get; set; }
        public IEnumerable<Object.ProductOfBill> productOfBills { get; set; }
    }

    public class ProductOfBill
    {
        public int productId { get; set; }
        public string pic { get; set; }
        public string productName { get; set; }
        public int? amount { get; set; }
        public int? price { get; set; }
    }
}