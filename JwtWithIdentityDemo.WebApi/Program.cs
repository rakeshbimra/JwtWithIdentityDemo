using JwtWithIdentityDemo.Application.IoC;



var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

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

