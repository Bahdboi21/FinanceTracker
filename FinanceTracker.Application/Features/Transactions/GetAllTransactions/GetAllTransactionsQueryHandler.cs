using AutoMapper;
using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinanceTracker.Application.Features.Transactions.GetAllTransactions
{
    public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, ResponseModel<List<TransactionResponse>>>
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllTransactionsQueryHandler(IRepository<Transaction> transactionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<List<TransactionResponse>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return ResponseModel<List<TransactionResponse>>.FailResponse("Unauthorized", 401);

            var transactions = await _transactionRepository.GetAsyncUserId(Guid.Parse(userId));
            var dtoList = _mapper.Map<List<TransactionResponse>>(transactions);

            return ResponseModel<List<TransactionResponse>>.SuccessResponse(dtoList, "Fetched successfully");
        }
    }

}
