using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Queries;

public class GetMembersQuery : IRequest<IEnumerable<Member>>
{
    public class GetMembersQueryHandler(IMemberDapperRepository repository)
        : IRequestHandler<GetMembersQuery, IEnumerable<Member>>
    {
        public async Task<IEnumerable<Member>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await repository.GetMembers();
            return members;
        }
    }
}