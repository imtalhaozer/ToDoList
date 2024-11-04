using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>
{
    public string UserId { get; set; } 
    public string Username { get; set; }
    public string Email { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception("User not found"); //burayı düzelt
        }
        
        user.UserName = request.Username;
        user.Email = request.Email;
        
        var result = await _userManager.UpdateAsync(user);
        
        UpdatedUserResponse response = _mapper.Map<UpdatedUserResponse>(user);
        return response;
    }
}