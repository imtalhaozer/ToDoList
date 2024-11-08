using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.GetById;
using Application.Features.Categories.Queries.GetList;
using AutoMapper;
using Core.Paging;
using Core.Response;
using Domain.Entities;

namespace Application.Features.Categories.Profiles;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, CreatedCategoryResponse>().ReverseMap();

        CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Category, UpdatedCategoryResponse>().ReverseMap();

        CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
        CreateMap<Category, DeletedCategoryResponse>().ReverseMap();

        CreateMap<Category, GetListCategoryListDto>().ReverseMap();
        CreateMap<Category, GetByIdCategoryResponse>().ReverseMap();
        CreateMap<Paginate<Category>, GetListResponse<GetListCategoryListDto>>().ReverseMap();
    } 
}