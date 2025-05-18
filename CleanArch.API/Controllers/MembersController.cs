using CleanArch.Application.Members.Commands;
using CleanArch.Application.Members.Queries;
using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMembers(GetMembersQuery query)
    {
        var members = await mediator.Send(query);
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember([FromRoute] int id)
    {
        var query = new GetMemberByIdQuery { Id = id };
        var member = await mediator.Send(query);
        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember(CreateMemberCommand command)
    {
        var member = await mediator.Send(command);
        return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember([FromRoute] int id)
    {
        var command = new UpdateMemberCommand { Id = id };
        var member = await mediator.Send(command);
        return Ok(member);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember([FromRoute] int id)
    {
        var command = new DeleteMemberCommand { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}