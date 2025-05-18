using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Commands;

public class UpdateMemberCommand : MemberCommandBase
{
    public int Id { get; set; }

    public class UpdateMemberCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateMemberCommand, Member>
    {
        public async Task<Member> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await unitOfWork.MemberRepository.GetMemberById(request.Id);
            if (member is null)
                throw new KeyNotFoundException($"Member with ID {request.Id} not found.");
            member.Update(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);
            unitOfWork.MemberRepository.UpdateMember(member);
            await unitOfWork.CommitAsync();
            return member;
        }
    }
}