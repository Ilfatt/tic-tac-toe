using Api.Endpoints.Game.Create;
using Api.Endpoints.Game.GetAll;
using Api.Endpoints.Game.Join;
using Api.Endpoints.User.Login;
using Api.Endpoints.User.Registration;
using AutoMapper;
using Core.Features.Game.Create;
using Core.Features.Game.GetAll;
using Core.Features.Game.Join;
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
		
		CreateMap<CreateGameRequest, CreateGameCommand>();
		CreateMapFromResult<CreateGameResult, CreateGameResponse>();
		CreateMapFromResult<GetAllGamesResult, GetAllGamesResponse>();
		
		CreateMap<JoinGameRequest, JoinGameCommand>();
		CreateMapFromResult<JoinGameResult, JoinGameResponse>();

		CreateMap<UserLoginRequest, UserLoginQuery>();
		CreateMapFromResult<UserLoginResult, UserLoginResponse>();
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