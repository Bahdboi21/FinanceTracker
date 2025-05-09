using AutoMapper;
using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinanceTracker.Application.Features.Transactions.UpdateTransaction
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, ResponseModel<TransactionResponse>>
    {
        private readonly IRepository<Transaction> _transactionRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UpdateTransactionCommandHandler(
            IRepository<Transaction> transactionRepo,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ResponseModel<TransactionResponse>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return ResponseModel<TransactionResponse>.FailResponse("Unauthorized.", 401);
            }

            var transaction = await _transactionRepo.GetAsync(request.Id);
            if (transaction == null || transaction.UserId != Guid.Parse(userId))
            {
                return ResponseModel<TransactionResponse>.FailResponse("Transaction not found.", 404);
            }

            // Update properties
            transaction.Title  =  request.TransactionDto.Title;
            transaction.Amount = request.TransactionDto.Amount;
            //transaction.Description = request.Description;
            transaction.Category = request.TransactionDto.Category;
            transaction.Date = request.TransactionDto.Date;

            await _transactionRepo.UpdateAsync(transaction);
            await _transactionRepo.SaveChanges();

            var transactionDto = _mapper.Map<TransactionResponse>(transaction);
            return ResponseModel<TransactionResponse>.SuccessResponse(transactionDto, "Transaction updated successfully.", 200);
        }
    }

}
