using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class GetByIdUserQuery : IRequest<User>
{
    public string UserId { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public GetByIdUserQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<User> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }
    }
}
