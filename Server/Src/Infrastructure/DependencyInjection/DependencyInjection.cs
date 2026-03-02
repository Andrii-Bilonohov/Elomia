using Application.Abstractions.Repositories;
using Application.Abstractions.Services.Ai;
using Application.Abstractions.Services.Chat;
using Application.Abstractions.Services.UnitOfWork;
using Application.Services.Ai;
using Application.Services.Chat;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ElomiaContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ElomiaContext>());
        
        services.AddScoped<IAiService, AiService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
        
        return services;
    }
}