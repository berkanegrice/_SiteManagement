using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.Requests;
using SiteManagement.Application.Common.Models.Requests.File;
using SiteManagement.Application.Files.Queries.GetFiles;

namespace SiteManagement.Application.Reports.Queries;

public record GetAllReportsQuery : IRequest<IQueryable<FileOnDataBaseDto>>
{
    // it may have a entities in future..
} 

public class GetAllReportsCommandHandler
    : IRequestHandler<GetAllReportsQuery, IQueryable<FileOnDataBaseDto>>
{
    private readonly IFileService _fileService;

    public GetAllReportsCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    public async Task<IQueryable<FileOnDataBaseDto>>
        Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
    {
        return await _fileService.FetchFileByCategory(new FetchFileRequest() { Type = "Report" });
    }
}