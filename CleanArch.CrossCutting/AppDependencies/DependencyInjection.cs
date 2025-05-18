using System.Data;
using System.Reflection;
using CleanArch.Application.Members.Commands.Validators;
using CleanArch.Domain.Abstract;
using CleanArch.Infrastructure.Context;
using CleanArch.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace CleanArch.CrossCutting.AppDependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new MySqlConnection(mySqlConnection);
            connection.Open();
            return connection;
        });
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IMemberDapperRepository, MemberDapperRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        var myHandlers = AppDomain.CurrentDomain.Load("CleanArch.Application");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myHandlers);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.Load("CleanArch.Application"));
        return services;
    }
}