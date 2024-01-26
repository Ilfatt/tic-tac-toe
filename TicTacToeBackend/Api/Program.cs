using Api.Events;
using Api.Extensions;
using Api.Hubs;
using Api.Mappings;
using Core.Features.User.Registration;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.AddSwagger();
builder.ConfigurePostgresqlConnection();
builder.AddMasstransitRabbitMq();
builder.AddAuthorization();

builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IEventMessageHandler, GameEventMessageHandler>();
builder.Services.AddCors();
builder.Services.AddMongoDb(builder.Configuration);

var validatorsAssembly = typeof(UserRegistrationValidator).Assembly;
var thisAssembly = typeof(Program).Assembly;
builder.Services.AddValidatorsFromAssembly(validatorsAssembly);
builder.Services.AddValidatorsFromAssembly(thisAssembly);
builder.Services.AddValidationBehaviorsFromAssembly(validatorsAssembly);
builder.Services.AddMediatR(x => 
	x.RegisterServicesFromAssemblies(validatorsAssembly, thisAssembly));
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

app.MapHub<GamesHub>("/api/room");

app.Run();