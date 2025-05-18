using CleanArch.Domain.Abstract;
using MediatR;

namespace CleanArch.Application.Members.Commands;

public class DeleteMemberCommand : IRequest
{
    public int Id { get; set; }
    
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.MemberRepository.DeleteMember(request.Id);
            await _unitOfWork.CommitAsync();
        }
    }
}