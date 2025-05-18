using CleanArch.Application.Members.Commands;
using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public MembersController(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var members = await _unitOfWork.MemberRepository.GetMembers();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember([FromRoute] int id)
    {
        var member = await _unitOfWork.MemberRepository.GetMemberById(id);
        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember(CreateMemberCommand command)
    {
        var member = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMember), new { id = member.Id }, member);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(UpdateMemberCommand command)
    {
        try
        {
            var member = await _mediator.Send(command);
            return Ok(member);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(DeleteMemberCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
}