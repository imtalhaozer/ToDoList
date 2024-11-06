using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _mapper = mapper;
            }

            public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                User user = _mapper.Map<User>(request);

                var roleExist = await _roleManager.RoleExistsAsync("User");
                if (!roleExist)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("User"));
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    }
                }

                var result = await _userManager.CreateAsync(user, request.Password);

                var addRole = await _userManager.AddToRoleAsync(user, "User");

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                return user;
            }
        }
    }
}
