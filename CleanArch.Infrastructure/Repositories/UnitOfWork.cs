using CleanArch.Domain.Abstract;
using CleanArch.Infrastructure.Context;

namespace CleanArch.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IMemberRepository? _memberRepository;
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IMemberRepository MemberRepository
    {
        get { return _memberRepository ??= new MemberRepository(_dbContext); }
    }

    public async Task CommitAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}