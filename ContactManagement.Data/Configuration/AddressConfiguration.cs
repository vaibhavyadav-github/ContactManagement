using ContactManagement.Data.Models;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManagement.Data.Configuration
{
    public class AddressConfiguration
    {
        public AddressConfiguration(EntityTypeBuilder<Address> entityBuilder)
        {
            entityBuilder.HasKey(t => t.UniqueId);
            entityBuilder.HasOne(e => e.Contact).WithOne(e => e.Address);
        }
    }
}
