using Application.Features.Users.Commands.Delete;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class DeleteUserCommand : IRequest<DeletedUserResponse>
{
    public Guid Id { get; set; }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with Id '{request.Id}' not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to delete user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            DeletedUserResponse response = _mapper.Map<DeletedUserResponse>(user);
            return response;
        }
    }
}
