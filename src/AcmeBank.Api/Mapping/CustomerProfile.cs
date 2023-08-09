using AcmeBank.Api.Endpoints.Customers;
using AcmeBank.Persistence.Entities;
using AutoMapper;

namespace AcmeBank.Api.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CreateCustomerRequest, Customer>()
                .ForMember(c => c.Status, request => request.MapFrom(r => r.IsActive))
                .ForMember(c => c.Gender, request => request.MapFrom(r => (int)r.Gender)
            );
            CreateMap<Customer, CreateCustomerResult>()
                .ForMember(ccr => ccr.Id, customer => customer.MapFrom(c => c.PersonId))
                .ForMember(ccr => ccr.IsActive, customer => customer.MapFrom(c => c.Status))
                .ForMember(ccr => ccr.Gender, customer => customer.MapFrom(c => (Gender)c.Gender)
            );
            CreateMap<Customer, PatchCustomerDocument>()
                .ForMember(ccr => ccr.Id, customer => customer.MapFrom(c => c.PersonId))
                .ForMember(ccr => ccr.IsActive, customer => customer.MapFrom(c => c.Status))
                .ForMember(ccr => ccr.Gender, customer => customer.MapFrom(c => (Gender)c.Gender)
            );
            CreateMap<PatchCustomerDocument, Customer>()
                .ForMember(c => c.Status, request => request.MapFrom(r => r.IsActive))
                .ForMember(c => c.Gender, request => request.MapFrom(r => (int)r.Gender)
            );

            CreateMap<Customer, CustomerResult>()
                .ForMember(ccr => ccr.Id, customer => customer.MapFrom(c => c.PersonId))
                .ForMember(ccr => ccr.IsActive, customer => customer.MapFrom(c => c.Status))
                .ForMember(ccr => ccr.Gender, customer => customer.MapFrom(c => (Gender)c.Gender)
            );

            CreateMap<UpdateCustomerRequest, Customer>()
                .ForMember(c => c.Status, request => request.MapFrom(r => r.Body.IsActive))
                .ForMember(c => c.Gender, request => request.MapFrom(r => (int)r.Body.Gender)
            );
            CreateMap<Customer, UpdateCustomerResult>()
                .ForMember(ccr => ccr.Id, customer => customer.MapFrom(c => c.PersonId))
                .ForMember(ccr => ccr.IsActive, customer => customer.MapFrom(c => c.Status))
                .ForMember(ccr => ccr.Gender, customer => customer.MapFrom(c => (Gender)c.Gender)
            );
            CreateMap<Customer, PatchCustomerResult>()
                .ForMember(ccr => ccr.Id, customer => customer.MapFrom(c => c.PersonId))
                .ForMember(ccr => ccr.IsActive, customer => customer.MapFrom(c => c.Status))
                .ForMember(ccr => ccr.Gender, customer => customer.MapFrom(c => (Gender)c.Gender)
            );
        }
    }
}
