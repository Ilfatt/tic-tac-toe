using Core.MessageConsumers;
using MassTransit;

namespace Api.Extensions;

public static class RabbitMqConfiguration
{
	public static void AddMasstransitRabbitMq(this WebApplicationBuilder applicationBuilder)
	{
		applicationBuilder.Services.AddMassTransit(config =>
		{
			config.AddConsumer<MoveHandlerConsumer>();

			config.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host(applicationBuilder.Configuration["RabbitMqConfig:Hostname"]);
				cfg.ConfigureEndpoints(ctx);
			});
		});
	}
}