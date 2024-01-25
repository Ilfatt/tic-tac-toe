using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigurePostgresqlConnection();
builder.AddMasstransitRabbitMq();

builder.Services.AddMongoDb(builder.Configuration);

var app = builder.Build();

await app.MigrateDb();

app.MapGet("/", () => "Hello World!");

app.Run();