using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Commands;

public class UpdateMemberCommand : IRequest<Member>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }

    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Member>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMemberCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Member> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.MemberRepository.GetMemberById(request.Id);
            if (member is null)
                throw new KeyNotFoundException($"Member with ID {request.Id} not found.");
            member.Update(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);
            _unitOfWork.MemberRepository.UpdateMember(member);
            await _unitOfWork.CommitAsync();
            return member;
        }
    }
}