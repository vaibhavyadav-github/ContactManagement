using ContactManagement.Data.Models;
using ContactManagement.Models.Dto;

using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Test
{
    public static class SeedData
    {
        public static ContactRequest ContactRequest()
        {
            return new ContactRequest
            {
                FirstName = "paul",
                LastName = "joy",
                Company = "tcs",
                Title = "Mr",
                Address = new AddressRequest
                {
                    City = "lko",
                    Country = "UK",
                    Postal = 120203,
                    Street = "st. peter"
                },
                ContactDetails = new List<ContactDetailRequest>
                {
                    new ContactDetailRequest
                    {
                        ContactType = Models.Enum.ContactType.Email,
                        Value = "aa@hh.com"
                    }
                }

            };
        }

        public static Contact Contact()
        {
            return new Contact
            {
                FirstName = "paul",
                LastName = "joy",
                Company = "tcs",
                Title = "Mr",
                Address = new Address
                {
                    City = "lko",
                    Country = "UK",
                    Postal = 120203,
                    Street = "st. peter"
                },
                ContactDetails = new List<ContactDetail>
                {
                    new ContactDetail
                    {
                        ContactType = Models.Enum.ContactType.Email,
                        Value = "aa@hh.com"
                    }
                }
            };
        }

    }
}
