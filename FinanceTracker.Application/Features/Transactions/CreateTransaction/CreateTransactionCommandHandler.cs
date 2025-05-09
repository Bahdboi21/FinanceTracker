using AutoMapper;
using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinanceTracker.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, ResponseModel<TransactionResponse>>
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateTransactionCommandHandler(IRepository<Transaction> transactionRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<TransactionResponse>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return ResponseModel<TransactionResponse>.FailResponse("Unauthorized", 401);

            var transaction = _mapper.Map<Transaction>(request.TransactionRequest);
            transaction.UserId = Guid.Parse(userId);

            var createdTransaction = await _transactionRepository.AddAsync(transaction);
            await _transactionRepository.SaveChanges();
            var responseDto = _mapper.Map<TransactionResponse>(createdTransaction);

            return ResponseModel<TransactionResponse>.SuccessResponse(responseDto, "Transaction created successfully");
        }
    }

}
