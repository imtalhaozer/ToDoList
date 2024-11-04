using Application.Features.ToDos.Commands.Create;
using Application.Features.Todos.Commands.Delete;
using Application.Features.Todos.Commands.Update;
using Application.Features.Todos.Queries.GetById;
using Application.Features.Todos.Queries.GetList;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User,Admin")]
    public class TodosController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTodoCommand createTodoCommand)
        {
            CreatedTodoResponse response = await Mediator.Send(createTodoCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTodoQuery getListTodoQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListTodoListDto> response = await Mediator.Send(getListTodoQuery);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdTodoQuery getByIdTodoQuery = new() { Id = id };
            GetByIdTodoResponse response = await Mediator.Send(getByIdTodoQuery);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTodoCommand updateTodoCommand)
        {
            UpdatedTodoResponse response = await Mediator.Send(updateTodoCommand);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            DeletedTodoResponse response = await Mediator.Send(new DeleteTodoCommand { Id = id });
            return Ok(response);
        }
    }
}
