using JwtWithIdentityDemo.Application.IoC;
using JwtWithIdentityDemo.Infrastructure.IoC;
using JwtWithIdentityDemo.WebApi.Models.Users.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    });

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddUserModelValidators();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();

    // Authentication & Authorization
    app.UseAuthentication();
    app.UseAuthorization();
    app.Run();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}




