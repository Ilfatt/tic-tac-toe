using Infrastructure;

namespace Features.GetAllUsersByTag;

public record GetAllUsersByTagRequest(string TagValue, string TagDomain) : IHttpRequest<GetAllUsersByTagResponse>;