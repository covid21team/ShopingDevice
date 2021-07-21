﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TSP_Covid21.Models.ShopEntity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class COVIDEntities : DbContext
    {
        public COVIDEntities()
            : base("name=COVIDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACCOUNT> ACCOUNT { get; set; }
        public virtual DbSet<ACCOUNTLIKE> ACCOUNTLIKE { get; set; }
        public virtual DbSet<ADDRESS_SHIP> ADDRESS_SHIP { get; set; }
        public virtual DbSet<BILLDETAIL> BILLDETAIL { get; set; }
        public virtual DbSet<BRAND> BRAND { get; set; }
        public virtual DbSet<CART> CART { get; set; }
        public virtual DbSet<COMMENT> COMMENT { get; set; }
        public virtual DbSet<CONFIG> CONFIG { get; set; }
        public virtual DbSet<CONFIGDETAIL> CONFIGDETAIL { get; set; }
        public virtual DbSet<PRODUCT> PRODUCT { get; set; }
        public virtual DbSet<PRODUCTTYPE> PRODUCTTYPE { get; set; }
        public virtual DbSet<RATINGPRODUCT> RATINGPRODUCT { get; set; }
        public virtual DbSet<TEMPPRODUCT> TEMPPRODUCT { get; set; }
        public virtual DbSet<VIEWNUMBER> VIEWNUMBER { get; set; }
        public virtual DbSet<VOCHERDETAIL> VOCHERDETAIL { get; set; }
        public virtual DbSet<VOUCHER> VOUCHER { get; set; }
        public virtual DbSet<ACCOUNT_ADMIN> ACCOUNT_ADMIN { get; set; }
        public virtual DbSet<BILL> BILL { get; set; }
    
        public virtual ObjectResult<string> CheckLogin(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("CheckLogin", usernameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<string> CheckLoginAdmin(string username, string password)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("CheckLoginAdmin", usernameParameter, passwordParameter);
        }
    }
}