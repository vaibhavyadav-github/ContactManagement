

namespace ContactManagement.Data.Models
{
    using ContactManagement.Data.Models.BaseClass;

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Contact : EntityBase
    {       
        public string Title { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Company { get; set; }

        public virtual ICollection<ContactDetail> ContactDetails { get; set; }

        public virtual Address Address { get; set; }
    }
}
