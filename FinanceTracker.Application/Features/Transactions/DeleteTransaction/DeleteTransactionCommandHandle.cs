using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinanceTracker.Application.Features.Transactions.DeleteTransaction
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, ResponseModel<string>>
    {
        private readonly IRepository<Transaction> _transactionRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteTransactionCommandHandler(
            IRepository<Transaction> transactionRepo,
            IHttpContextAccessor httpContextAccessor)
        {
            _transactionRepo = transactionRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<string>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return ResponseModel<string>.FailResponse("Unauthorized.", 401);
            }

            var transaction = await _transactionRepo.GetAsync(request.Id);
            if (transaction == null || transaction.UserId != Guid.Parse(userId))
            {
                return ResponseModel<string>.FailResponse("Transaction not found.", 404);
            }

            await _transactionRepo.DeleteAsync(transaction);
            return ResponseModel<string>.SuccessResponse("Transaction deleted successfully.", "Success", 200);
        }
    }

}
