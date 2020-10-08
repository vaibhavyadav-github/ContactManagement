using ContactManagement.Data.Models.BaseClass;

namespace ContactManagement.Data.Models
{
    public class Address : EntityBase
    {       
        public string City { get; set; }

        public string Country { get; set; }

        public string Street { get; set; }

        public int Postal { get; set; }

        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

    }
}
