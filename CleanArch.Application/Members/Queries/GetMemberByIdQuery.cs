using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Queries;

public class GetMemberByIdQuery : IRequest<Member>
{
    public int Id { get; init; }
    
    public class GetMemberByIdQueryHandler(IMemberDapperRepository repository)
        : IRequestHandler<GetMemberByIdQuery, Member>
    {
        public async Task<Member> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await repository.GetMemberById(request.Id);
            if (member is null)
                throw new KeyNotFoundException($"Member with ID {request.Id} not found.");
            return member;
        }
    }
}