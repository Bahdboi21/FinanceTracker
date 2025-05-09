using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Auth.LoginUser
{
    public class LoginUserCommand : IRequest<ResponseModel<AuthResponse>>
    {
        public LoginRequest Request { get; set; }

        public LoginUserCommand(LoginRequest request)
        {
            Request = request;
        }
    }

}
