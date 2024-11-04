namespace Application.Features.Categories.Queries.GetById;

public class GetByIdCategoryResponse
{
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}