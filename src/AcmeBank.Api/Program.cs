using AcmeBank.Contracts;
using AcmeBank.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext("Name=ConnectionStrings:AcmeBank");
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddProblemDetails();
builder.Services.AddAutoMapper(typeof(Program));

//TODO: Move to extension menthods
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Acme Bank API",
        Description = "An API for managing customers, accounts and movements",
        TermsOfService = new Uri("https://engineering.acmebank.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Engineering",
            Url = new Uri("https://engineering.acmebank.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://engineering.acmebank.com/license")
        }
    });
    options.EnableAnnotations();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error");
}
//TODO: Add config for prod env. E.g. cors


app.Map("/error", (IHttpContextAccessor httpContextAccessor) =>
{
    Exception? exception = httpContextAccessor.HttpContext?
        .Features.Get<IExceptionHandlerFeature>()?
        .Error;

    //TODO: Log exception
    return exception is BusinessLogicException e
        ? Results.Problem(
            title: e.Message,
            statusCode: StatusCodes.Status428PreconditionRequired)
        : Results.Problem(
            title: "Internal error",
            statusCode: StatusCodes.Status500InternalServerError);
});

app.MapControllers();
//TODO: Add health checks
//TODO: Add security
//TODO: Add versioning
app.Logger.LogInformation("Starting web host ({ApplicationName})...", "AcmeBank API");
app.Run();
