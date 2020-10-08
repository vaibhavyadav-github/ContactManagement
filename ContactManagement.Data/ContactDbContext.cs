using ContactManagement.Data.Configuration;
using ContactManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactManagement.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
      : base(options)
        { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<ContactDetail> ContactDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ContactConfiguration(modelBuilder.Entity<Contact>());
            new AddressConfiguration(modelBuilder.Entity<Address>());
            new ContactDetailConfiguration(modelBuilder.Entity<ContactDetail>());

        }
    }
}
