﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OlxProject
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OlxDbEntities : DbContext
    {
        public OlxDbEntities()
            : base("name=OlxDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<AddAttribute> AddAttributes { get; set; }
        public virtual DbSet<FavoriteAd> FavoriteAds { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
    }
}