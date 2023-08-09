using AcmeBank.Api.Endpoints.Accounts;
using AcmeBank.Persistence.Entities;
using AutoMapper;

namespace AcmeBank.Api.Mapping
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountRequest, Account>()
                .ForMember(c => c.Status, request => request.MapFrom(r => r.IsActive))
                .ForMember(c => c.Type, request => request.MapFrom(r => (int)r.Type)
            );
            CreateMap<Account, CreateAccountResult>()
                .ForMember(ccr => ccr.IsActive, customer => customer.MapFrom(c => c.Status))
                .ForMember(ccr => ccr.Type, customer => customer.MapFrom(c => (AccountType)c.Type)
            );
            CreateMap<Account, AccountResult>()
                .ForMember(ccr => ccr.IsActive, customer => customer.MapFrom(c => c.Status))
                .ForMember(ccr => ccr.Type, customer => customer.MapFrom(c => (AccountType)c.Type)
            );
        }
    }
}
