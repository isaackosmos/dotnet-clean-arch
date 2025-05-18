using CleanArch.Application.Members.Commands.Notifications;
using CleanArch.Application.Members.Commands.Validators;
using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CleanArch.Application.Members.Commands;

public class CreateMemberCommand : MemberCommandBase
{
    public class CreateMemberCommandHandler(IUnitOfWork unitOfWork, IMediator mediator) : IRequestHandler<CreateMemberCommand, Member>
    {
        public async Task<Member> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = new Member(request.FirstName, request.LastName, request.Gender, request.Email, request.IsActive);
            await unitOfWork.MemberRepository.AddMember(member);
            await unitOfWork.CommitAsync();
            await mediator.Publish(new MemberCreatedNotification(member), cancellationToken);
            return member;
        }
    }
}