using Api.Extensions;
using Asp.Net;
using Data;
using Features.GetUser;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAppBuilder.Create();

builder.ConfigurePostgresqlConnection();
builder.AddEndpointsFromAssembly<Program>();

builder.ServiceCollection.AddScoped<IAppDbContext, AppDbContext>();
builder.ServiceCollection.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetUserRequest>());

var app = builder.Build();

await app.ServiceProvider.MigrateDb();

await app.RunAsync();