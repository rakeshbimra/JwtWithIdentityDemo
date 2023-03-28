using JwtWithIdentityDemo.Application.IoC;
using JwtWithIdentityDemo.Infrastructure.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddInfrastructure(builder.Configuration);
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
    app.UseSwaggerUI();
}

