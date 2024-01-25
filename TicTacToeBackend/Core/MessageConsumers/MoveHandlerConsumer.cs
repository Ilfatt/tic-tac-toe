using MassTransit;
using Model.Dto;

namespace Core.MessageConsumers;

/// <summary>
/// Обрабочик хода
/// </summary>
public class MoveHandlerConsumer : IConsumer<MoveDto>
{
	public Task Consume(ConsumeContext<MoveDto> context)
	{
		throw new NotImplementedException();
	}
}