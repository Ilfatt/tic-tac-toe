using Api.Events;
using Api.Extensions;
using Api.Mappings;
using Api.Services;
using Api.Services.Abstractions;
using Core.Features.User.Registration;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwagger();
builder.ConfigurePostgresqlConnection();
builder.AddMasstransitRabbitMq();
builder.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserService, UserService>()
	.AddScoped<IEventMessageHandler, IEventMessageHandler>();

builder.Services.AddCors();
builder.Services.AddMongoDb(builder.Configuration);

var validatorsAssembly = typeof(UserRegistrationValidator).Assembly;
builder.Services.AddValidatorsFromAssembly(validatorsAssembly);
builder.Services.AddValidationBehaviorsFromAssembly(validatorsAssembly);
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(validatorsAssembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

await app.MigrateDb();

app.UseAuthentication();
app.UseAuthorization();

app.RouteEndpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(option =>
{
	option.AllowAnyHeader();
	option.AllowAnyMethod();
	option.AllowAnyOrigin();
});

app.Run();