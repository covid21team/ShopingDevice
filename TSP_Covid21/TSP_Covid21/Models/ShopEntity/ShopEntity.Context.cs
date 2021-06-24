﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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

    public virtual DbSet<ACCOUNT> ACCOUNTs { get; set; }
    public virtual DbSet<ACCOUNT_ADMIN> ACCOUNT_ADMIN { get; set; }
    public virtual DbSet<ACCOUNTLIKE> ACCOUNTLIKEs { get; set; }
    public virtual DbSet<BILL> BILLs { get; set; }
    public virtual DbSet<BILLDETAIL> BILLDETAILs { get; set; }
    public virtual DbSet<BRAND> BRANDs { get; set; }
    public virtual DbSet<CART> CARTs { get; set; }
    public virtual DbSet<COMMENT> COMMENTs { get; set; }
    public virtual DbSet<CONFIG> CONFIGs { get; set; }
    public virtual DbSet<CONFIGDETAIL> CONFIGDETAILs { get; set; }
    public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
    public virtual DbSet<PRODUCTTYPE> PRODUCTTYPEs { get; set; }
    public virtual DbSet<TEMPPRODUCT> TEMPPRODUCTs { get; set; }
    public virtual DbSet<VOCHERDETAIL> VOCHERDETAILs { get; set; }
    public virtual DbSet<VOUCHER> VOUCHERs { get; set; }
    public virtual DbSet<RATINGPRODUCT> RATINGPRODUCTs { get; set; }
    public virtual DbSet<VIEWNUMBER> VIEWNUMBERs { get; set; }

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
}
