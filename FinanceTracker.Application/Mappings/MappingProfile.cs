using AutoMapper;
using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Request -> Entity
            CreateMap<CreateTransactionRequest, Transaction>();
            CreateMap<UpdateTransactionRequest, Transaction>();

            // Entity -> Response
            CreateMap<Transaction, TransactionResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}
