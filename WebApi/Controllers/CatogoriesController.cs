using Application.Features.Todos.Queries.GetList;
using Application.Features.Todos.Queries.GetListByCategory;
using Application.Features.Todos.Queries.GetListByDate;
using Application.Features.Todos.Queries.GetByStatus;
using Application.Services.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Queries.GetById;
using Application.Features.Categories.Queries.GetList;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand() { Id = id });
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetByIdCategoryQuery() { Id = id });
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetListCategoryQuery()
            {
                PageRequest = new Core.Requests.PageRequest { PageIndex = pageIndex, PageSize = pageSize }
            });

            return Ok(result);
        }
        
        [HttpGet("todosByCategory")]
        public async Task<IActionResult> GetTodosByCategory([FromQuery] List<int> categoryIds, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetListByCategoryTodoQuery
            {
                CategoryIds = categoryIds,
                PageRequest = new PageRequest { PageIndex = pageIndex, PageSize = pageSize }
            });

            return Ok(result);
        }
        
        [HttpGet("todosByDate")]
        public async Task<IActionResult> GetTodosByDate([FromQuery] DateTime date, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetListByDateTodoQuery
            {
                Date = date,
                PageRequest = new PageRequest { PageIndex = pageIndex, PageSize = pageSize }
            });

            return Ok(result);
        }
        
        [HttpGet("todosByPriority")]
        public async Task<IActionResult> GetTodosByPriority([FromQuery] string status, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetTodosByPriorityQuery
            {
                Status = status,
                PageRequest = new PageRequest { PageIndex = pageIndex, PageSize = pageSize }
            });

            return Ok(result);
        }
        
        [HttpGet("todosByStatus")]
        public async Task<IActionResult> GetTodosByStatus([FromQuery] bool isPastDue)
        {
            var result = await _mediator.Send(new GetByStatusTodoQuery
            {
                IsPastDue = isPastDue
            });

            return Ok(result);
        }
    }
}
