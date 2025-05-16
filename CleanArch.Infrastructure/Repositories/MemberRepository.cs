using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using CleanArch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    protected readonly AppDbContext Db;

    public MemberRepository(AppDbContext _db)
    {
        Db = _db;
    }

    public async Task<IEnumerable<Member>> GetMembers()
    {
        var members = await Db.Members.ToListAsync();
        return members;
    }

    public async Task<Member> GetMemberById(int memberId)
    {
        var member = await Db.Members.FindAsync(memberId);
        if (member is null)
            throw new InvalidOperationException("Member not found");
        return member;
    }

    public async Task<Member> AddMember(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));
        await Db.Members.AddAsync(member);
        return member;
    }

    public void UpdateMember(Member member)
    {
        if (member is null)
            throw new ArgumentNullException(nameof(member));
        Db.Members.Update(member);
    }

    public async Task DeleteMember(int memberId)
    {
        var member = await GetMemberById(memberId);
        if (member is null)
            throw new InvalidOperationException("Member not found");
        Db.Members.Remove(member);
    }
}