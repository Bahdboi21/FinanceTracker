using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.GetAllTransactions
{
    public class GetAllTransactionsQuery : IRequest<ResponseModel<List<TransactionResponse>>> { }

}
