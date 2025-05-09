using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;

namespace FinanceTracker.Application.Features.Auth.CreateUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseModel<AuthResponse>>
    {
        private readonly IRepository<User> _userRepo;
        private readonly IAuthService _authService;

        public RegisterUserCommandHandler(IRepository<User> userRepo, IAuthService authService)
        {
            _userRepo = userRepo;
            _authService = authService;
        }

        public async Task<ResponseModel<AuthResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userRepo.AnyAsync(x => x.Email ==  request.Request.Email);
            if (existing)
                return ResponseModel<AuthResponse>.FailResponse("User already exists", 400);

            var user = new User
            {
                Email = request.Request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Request.Password)
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChanges();
            var token = _authService.GenerateToken(user);

            return ResponseModel<AuthResponse>.SuccessResponse(new AuthResponse { Email = user.Email, Token = token }, "User registered");
        }
    }

}
