

namespace ContactManagement.Data.Models
{
    using ContactManagement.Data.Models.BaseClass;
    using ContactManagement.Models.Enum;

    public class ContactDetail : EntityBase
    {
        public string Value { get; set; }
        public ContactType ContactType { get; set; }

        public int ContactId { get; set; }
        public virtual Contact Contact { get; set; }

    }
}
