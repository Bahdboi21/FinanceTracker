using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Auth.CreateUser
{
    public class RegisterUserCommand : IRequest<ResponseModel<AuthResponse>>
    {
        public RegisterRequest Request { get; set; }

        public RegisterUserCommand(RegisterRequest request)
        {
            Request = request;
        }
    }

}
