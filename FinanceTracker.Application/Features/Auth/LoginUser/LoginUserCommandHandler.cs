using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Features.Auth.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResponseModel<AuthResponse>>
    {
        private readonly IRepository<User> _userRepo;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IRepository<User> userRepo, IAuthService authService)
        {
            _userRepo = userRepo;
            _authService = authService;
        }

        public async Task<ResponseModel<AuthResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetBySpec(x => x.Email == request.Request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Request.Password, user.PasswordHash))
                return ResponseModel<AuthResponse>.FailResponse("Invalid credentials", 401);

            var token = _authService.GenerateToken(user);
            return ResponseModel<AuthResponse>.SuccessResponse(new AuthResponse { Email = user.Email, Token = token }, "Login successful");
        }
    }

}
