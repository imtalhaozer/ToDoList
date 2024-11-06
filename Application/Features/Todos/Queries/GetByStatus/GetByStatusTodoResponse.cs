namespace Application.Features.Todos.Queries.GetByStatus
{
    public class GetByStatusTodoResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EndDate { get; set; }
        public bool Completed { get; set; }
    }
}