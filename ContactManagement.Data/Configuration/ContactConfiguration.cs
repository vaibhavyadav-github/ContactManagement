using ContactManagement.Data.Models;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManagement.Data.Configuration
{
    public class ContactConfiguration
    {
        public ContactConfiguration(EntityTypeBuilder<Contact> entityBuilder)
        {
            entityBuilder.HasKey(t => t.UniqueId);
            entityBuilder.HasIndex(p => new { p.FirstName, p.LastName }).IsUnique();
            entityBuilder.Property(t => t.FirstName).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.LastName).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.Title).HasMaxLength(100).IsRequired();
            entityBuilder.Property(t => t.Company).HasMaxLength(100).IsRequired();
        }
    }
}
