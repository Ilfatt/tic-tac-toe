using Api.Endpoints.User.Registration;
using AutoMapper;
using Core.Features.User.Registration;
using MediatR;

namespace Api.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<UserRegistrationRequest, UserRegistrationCommand>();
		CreateMapFromResult<UserRegistrationResult, UserRegistrationResponse>();
	}

	private void CreateMapFromResult<TSource, TDestination>()
	{
		CreateMap<TSource, TDestination>();
		CreateMap<Result<TSource>, TDestination>()
			.AfterMap((src, dest, context) =>
			{
				if (src.Body != null) context.Mapper.Map(src.Body, dest);
			});
	}
}