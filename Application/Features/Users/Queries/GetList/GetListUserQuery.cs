using AutoMapper;
using Domain.Entities; // IdentityUser burada tanımlı olmalıdır.
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<List<GetListUserListItemDto>>
{
}

public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, List<GetListUserListItemDto>>
{
    private readonly UserManager<User> _userManager; 
    private readonly IMapper _mapper;

    public GetListUserQueryHandler(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<List<GetListUserListItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync(cancellationToken); 
        var userDtos = _mapper.Map<List<GetListUserListItemDto>>(users);

        return userDtos;
    }
}