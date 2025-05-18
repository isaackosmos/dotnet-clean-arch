using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Abstract;

public interface IMemberDapperRepository
{
    Task<IEnumerable<Member>> GetMembers();
    Task<Member?> GetMemberById(int id);
}