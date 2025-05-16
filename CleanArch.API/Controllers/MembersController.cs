using CleanArch.Domain.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MembersController(IUnitOfWork unitOfWork)
    {
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
}