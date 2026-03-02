using Infrastructure.DependencyInjection;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load(Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", ".env")));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
