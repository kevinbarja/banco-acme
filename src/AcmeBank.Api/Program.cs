using AcmeBank.Contracts;
using AcmeBank.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext("Name=ConnectionStrings:AcmeBank");
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

var app = builder.Build();
app.Run();
