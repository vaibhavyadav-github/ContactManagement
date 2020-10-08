

namespace ContactManagement.Web.MapperProfiles
{
    using AutoMapper;

    using ContactManagement.Data.Models;
    using ContactManagement.Models.Dto;

    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactDetailRequest, ContactDetail>();
            CreateMap<AddressRequest, Address>();
            CreateMap<ContactRequest, Contact>()
                .ForMember(dest => dest.ContactDetails, opts => opts.MapFrom(src => src.ContactDetails))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Address));
            
        }
    }
}
