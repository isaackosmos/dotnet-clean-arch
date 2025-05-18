using CleanArch.Domain.Abstract;
using MediatR;

namespace CleanArch.Application.Members.Commands;

public class DeleteMemberCommand : IRequest
{
    public int Id { get; init; }
    
    public class DeleteMemberCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMemberCommand>
    {
        public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.MemberRepository.DeleteMember(request.Id);
            await unitOfWork.CommitAsync();
        }
    }
}