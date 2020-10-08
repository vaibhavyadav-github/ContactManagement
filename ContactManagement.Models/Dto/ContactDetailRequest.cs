using ContactManagement.Models.Enum;

namespace ContactManagement.Models.Dto
{
    public class ContactDetailRequest
    {
        public string Value { get; set; }
        public ContactType ContactType { get; set; }

    }
}
