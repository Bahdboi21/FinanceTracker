using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.GetTransactions
{
    public class GetTransactionsQuery : IRequest<ResponseModel<List<TransactionResponse>>>
    {
        public GetTransactionRequest TransactionRequest { get; set; }

        public GetTransactionsQuery(GetTransactionRequest transactionRequest)
        {
            TransactionRequest = transactionRequest;
        }
    }
}
