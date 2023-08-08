using AcmeBank.Api;
using AcmeBank.Contracts;
using AcmeBank.Persistence;
using BackendData.Security;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext("Name=ConnectionStrings:AcmeBank");
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

var app = builder.Build();


app.UseExceptionHandler("/error");
app.Map("/error", (IHttpContextAccessor httpContextAccessor) =>
{
    Exception? exception = httpContextAccessor.HttpContext?
        .Features.Get<IExceptionHandlerFeature>()?
        .Error;

    //TODO: Log exception
    return exception is BusinessLogicException e
        ? Results.Problem(
            title: e.Message,
            statusCode: 213)
        : Results.Problem(
            title: "Internal error",
            statusCode: StatusCodes.Status500InternalServerError);
});

app.MapControllers();
//TODO: Add health checks
app.Logger.LogInformation("Starting web host ({ApplicationName})...", "AcmeBank API");
app.Run();
