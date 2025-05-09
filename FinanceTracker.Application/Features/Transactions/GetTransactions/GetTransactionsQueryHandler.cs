using AutoMapper;
using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.GetTransactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, ResponseModel<List<TransactionResponse>>>
    {
        private readonly IRepository<Transaction> _repo;
        private readonly IMapper _mapper;

        public GetTransactionsQueryHandler(IRepository<Transaction> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<TransactionResponse>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var data = await _repo.GetAllBySpec(t =>
                 t.UserId == request.TransactionRequest.UserId &&
                 (string.IsNullOrEmpty(request.TransactionRequest.Category) || t.Category == request.TransactionRequest.Category));


            var response = _mapper.Map<List<TransactionResponse>>(data);
            return ResponseModel<List<TransactionResponse>>.SuccessResponse(response, "Transactions fetched successfully");
        }
    }

}
