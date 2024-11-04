using Application.Features.Todos.Commands.Delete;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommand:IRequest<DeletedUserResponse>
{
    public Guid Id { get; set; }
    
    public class DeleteUserCommandHandler:IRequestHandler<DeleteUserCommand,DeletedUserResponse>
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
            await _userManager.DeleteAsync(user);

            DeletedUserResponse response = _mapper.Map<DeletedUserResponse>(user);
            return response;
        }
    }
}