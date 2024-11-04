using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities; 
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<GetByIdUserResponse>
{
    public string UserId { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public GetByIdUserQueryHandler(UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            IdentityUser user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);
            return response;
        }
    }
}