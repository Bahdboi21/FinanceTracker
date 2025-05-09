using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.CreateTransaction
{
    public class CreateTransactionCommand : IRequest<ResponseModel<TransactionResponse>>
    {
        public CreateTransactionRequest TransactionRequest { get; set; }

        public CreateTransactionCommand(CreateTransactionRequest request)
        {
            TransactionRequest = request;
        }
    }
}
