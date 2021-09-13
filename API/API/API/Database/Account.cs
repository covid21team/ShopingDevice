using System;
using System.Collections.Generic;

namespace API.Database
{
    public partial class Account
    {
        public Account()
        {
            Accountlike = new HashSet<Accountlike>();
            AddressShip = new HashSet<AddressShip>();
            Bill = new HashSet<Bill>();
            Cart = new HashSet<Cart>();
            Comment = new HashSet<Comment>();
            Ratingproduct = new HashSet<Ratingproduct>();
            Viewnumber = new HashSet<Viewnumber>();
            Vocherdetail = new HashSet<Vocherdetail>();
        }

        public string User { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public bool? Sex { get; set; }
        public DateTime? Dataofbirth { get; set; }
        public bool? Statusaccount { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }

        public ICollection<Accountlike> Accountlike { get; set; }
        public ICollection<AddressShip> AddressShip { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public ICollection<Cart> Cart { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<Ratingproduct> Ratingproduct { get; set; }
        public ICollection<Viewnumber> Viewnumber { get; set; }
        public ICollection<Vocherdetail> Vocherdetail { get; set; }
    }
}
