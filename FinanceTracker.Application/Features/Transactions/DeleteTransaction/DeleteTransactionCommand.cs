using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Transactions.DeleteTransaction
{
    public class DeleteTransactionCommand : IRequest<ResponseModel<string>>
    {
        public Guid Id { get; set; }

        public DeleteTransactionCommand(Guid id)
        {
            Id = id;
        }
    }

}
