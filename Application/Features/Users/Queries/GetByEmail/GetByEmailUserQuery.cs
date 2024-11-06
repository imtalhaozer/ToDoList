using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries.GetByEmail
{
    public class GetByEmailUserQuery : IRequest<User>
    {
        public string Email { get; set; }

        public class GetByEmailUserQueryHandler : IRequestHandler<GetByEmailUserQuery, User>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public GetByEmailUserQueryHandler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<User> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
            {
                IdentityUser identityUser = await _userManager.FindByEmailAsync(request.Email);

                if (identityUser == null)
                {
                    throw new Exception("User not found");
                }

                User user = _mapper.Map<User>(identityUser);

                return user;
            }
        }
    }
}
