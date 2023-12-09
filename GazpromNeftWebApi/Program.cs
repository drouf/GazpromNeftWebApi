using GazpromNeftWebApi.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using GazpromNeftWebApi.Db;
using Microsoft.EntityFrameworkCore;
using GazpromNeftWebApi;
using GazpromNeftWebApi.Requests;
using GazpromNeftWebApi.Handlers;
using Microsoft.AspNetCore.Hosting;
using GazpromNeftWebApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

//handlers
{
    builder.Services.AddScoped<IRequestHandler<CreateUserRequest, User?>, CreateUserHandler>();
    builder.Services.AddScoped<IRequestHandler<DeleteUserRequest, User?>, DeleteUserHandler>();
    builder.Services.AddScoped<IRequestHandler<GetUserRequest, IEnumerable<User>>, GetUserHandler>();
    builder.Services.AddScoped<IRequestHandler<UpdateUserRequest, User?>, UpdateUserHandler>();
}

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

// Add services to the container.

builder.Services.AddDbContext<GNContext>(ConfigureUserContextConnection);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

void ConfigureUserContextConnection(DbContextOptionsBuilder options)
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Development"))
        .EnableSensitiveDataLogging();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
