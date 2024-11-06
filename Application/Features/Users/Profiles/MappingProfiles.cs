using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Paging;
using Core.Response;
using Domain.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<User, CreateUserCommand>().ReverseMap();

        CreateMap<User, UpdateUserCommand>().ReverseMap();
        CreateMap<User, UpdatedUserResponse>().ReverseMap();

        CreateMap<User, DeleteUserCommand>().ReverseMap();
        CreateMap<User, DeletedUserResponse>().ReverseMap();

        CreateMap<User, GetListUserListItemDto>().ReverseMap();
        CreateMap<Paginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
    }
}