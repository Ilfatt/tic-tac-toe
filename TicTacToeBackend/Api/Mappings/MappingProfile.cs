using Api.Endpoints.User.GetUserData;
using Api.Endpoints.User.Login;
using Api.Endpoints.User.Registration;
using AutoMapper;
using Core.Features.User.GetUserData;
using Core.Features.User.Login;
using Core.Features.User.Registration;
using MediatR;

namespace Api.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<UserRegistrationRequest, UserRegistrationCommand>();
		CreateMapFromResult<UserRegistrationResult, UserRegistrationResponse>();

		CreateMap<UserLoginRequest, UserLoginQuery>();
		CreateMapFromResult<UserLoginResult, UserLoginResponse>();
		
		CreateMapFromResult<GetUserDataResult, GetUserDataResponse>();
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