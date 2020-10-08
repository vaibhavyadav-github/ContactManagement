
namespace ContactManagement.Models.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ContactRequest
    {
        [Required(ErrorMessage = "Please fill required field.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please fill required field.")]
        public string LastName { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public AddressRequest Address { get; set; }

        [Required, MinLength(1, ErrorMessage = "At least one entry required as contact detail.")]
        public List<ContactDetailRequest> ContactDetails { get; set; }
    }   
}
