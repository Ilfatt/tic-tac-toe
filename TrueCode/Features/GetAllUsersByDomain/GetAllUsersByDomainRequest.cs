using Infrastructure;

namespace Features.GetAllUsersByDomain;

public record GetAllUsersByDomainRequest(int PageSize, int PageNumber, string Domain) 
	: IHttpRequest<GetAllUsersByDomainResponse>;