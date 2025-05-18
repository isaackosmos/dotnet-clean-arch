using System.Data;
using CleanArch.Domain.Abstract;
using CleanArch.Domain.Entities;
using Dapper;

namespace CleanArch.Infrastructure.Repositories;

public class MemberDapperRepository(IDbConnection dbConnection) : IMemberDapperRepository
{
    public async Task<IEnumerable<Member>> GetMembers()
    {
        var query = "SELECT * FROM Members";
        return await dbConnection.QueryAsync<Member>(query);
    }

    public async Task<Member?> GetMemberById(int id)
    {
        var query = "SELECT * FROM Members WHERE Id = @Id";
        return await dbConnection.QueryFirstOrDefaultAsync<Member>(query, new { Id = id });
    }
}