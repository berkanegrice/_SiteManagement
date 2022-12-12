using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Domain.Entities;

namespace SiteManagement.Application.Users.Queries.GetUsers;

public record GetUserListQuery : IRequest<IQueryable<IdentityUser>>
{
    public string? UserId { get; init; }
}

public class GetUserListHandler : IRequestHandler<GetUserListQuery, IQueryable<IdentityUser>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetUserListHandler(UserManager<IdentityUser> userManager, IMapper mapper, IIdentityService identityService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
    }
    
    
    public Task<IQueryable<IdentityUser>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_userManager
            .Users
            .Where(x => x.Id != request.UserId));
    }
}