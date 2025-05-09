using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.UpdateTransaction
{
    public class UpdateTransactionCommand : IRequest<ResponseModel<TransactionResponse>>
    {
        public Guid Id { get; set; }
        public UpdateTransactionRequest TransactionDto { get; set; }

        public UpdateTransactionCommand(UpdateTransactionRequest transactionDto,Guid  id)
        {
            Id = id;
            TransactionDto = transactionDto;
        }
    }

}
