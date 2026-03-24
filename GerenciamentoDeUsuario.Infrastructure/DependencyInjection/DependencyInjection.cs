using GerenciamentoDeUsuario.Application.Interfaces;
using GerenciamentoDeUsuario.Application.Services;
using GerenciamentoDeUsuario.Domain.Interfaces;
using GerenciamentoDeUsuario.Infrastructure.Persistencie;
using GerenciamentoDeUsuario.Infrastructure.Repositories;
using GerenciamentoDeUsuario.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GerenciamentoDeUsuario.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services
        ,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
            )
        );

        services.AddScoped<IUserRepository,
            UserRepository>();
        services.AddScoped<IUnitOfWork>(provider =>
        provider.GetRequiredService<AppDbContext>());
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
