using Application.Features.ToDos.Commands.Create;
using Application.Features.Todos.Commands.Delete;
using Application.Features.Todos.Commands.Update;
using Application.Features.Todos.Queries.GetById;
using Application.Features.Todos.Queries.GetList;
using AutoMapper;
using Core.Paging;
using Core.Response;
using Domain.Entities;

namespace Application.Features.Todos.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Todo, CreateTodoCommand>().ReverseMap();
        CreateMap<Todo, CreatedTodoResponse>().ReverseMap();

        CreateMap<Todo, UpdateTodoCommand>().ReverseMap();
        CreateMap<Todo, UpdatedTodoResponse>().ReverseMap();

        CreateMap<Todo, DeleteTodoCommand>().ReverseMap();
        CreateMap<Todo, DeletedTodoResponse>().ReverseMap();

        CreateMap<Todo, GetListTodoListDto>().ReverseMap();
        CreateMap<Todo, GetByIdTodoResponse>().ReverseMap();
        CreateMap<Paginate<Todo>, GetListResponse<GetListTodoListDto>>().ReverseMap();
    }
}