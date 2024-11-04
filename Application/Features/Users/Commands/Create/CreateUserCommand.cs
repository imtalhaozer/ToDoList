using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommand:IRequest<CreatedUserResponse>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public class CreateUserCommandHandler:IRequestHandler<CreateUserCommand,CreatedUserResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);
            await _userManager.CreateAsync(user, request.Password);
            CreatedUserResponse createdUserResponse = _mapper.Map<CreatedUserResponse>(user);
            return createdUserResponse;
        }
    }
}