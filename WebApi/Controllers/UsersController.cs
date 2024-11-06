using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetList;
using Core.Requests;
using Core.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
        {
            var response = await Mediator.Send(createUserCommand);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            List<GetListUserListItemDto> response = await Mediator.Send(new GetListUserQuery());
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            GetByIdUserQuery getByIdUserQuery = new() { UserId = id };
            var response = await Mediator.Send(getByIdUserQuery);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            UpdatedUserResponse response = await Mediator.Send(updateUserCommand);
            return Ok(response);
        }
        
        [Authorize(Roles = "User,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteUserCommand = new DeleteUserCommand { Id = id };
            var response = await Mediator.Send(deleteUserCommand);
            return Ok(response);
        }
    }
}
