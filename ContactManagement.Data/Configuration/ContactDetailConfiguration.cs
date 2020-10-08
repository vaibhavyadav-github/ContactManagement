using ContactManagement.Data.Models;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManagement.Data.Configuration
{
    public class ContactDetailConfiguration
    {
        public ContactDetailConfiguration(EntityTypeBuilder<ContactDetail> entityBuilder)
        {
            entityBuilder.HasKey(t => t.UniqueId);
            entityBuilder.Property(t => t.Value).HasMaxLength(100).IsRequired();
            entityBuilder.HasOne(e => e.Contact).WithMany(e => e.ContactDetails).HasForeignKey(e => e.ContactId);

        }
    }
}
