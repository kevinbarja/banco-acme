using AcmeBank.Api.Endpoints.Accounts.Movements;
using AcmeBank.Persistence.Entities;
using AutoMapper;

namespace AcmeBank.Api.Mapping
{
    public class MovementProfile : Profile
    {
        public MovementProfile()
        {
            CreateMap<Movement, CreateMovementResult>()
                .ForMember(cmr => cmr.Type, customer => customer.MapFrom(c => (MovementType)c.Type)
            );
        }
    }
}
