using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models.Requests.File;
using SiteManagement.Application.DueRelated.DueInformations.Response;
using SiteManagement.Application.Files.Commands.UploadFiles;

namespace SiteManagement.Application.DueRelated.DueInformations.Command;


public record UploadDueListCommand
    : IRequest<ResponseUploadDueListCommand>
{
    public UploadFileCommand UploadFileCommand { get; set; }
}

public class UploadDueListCommandHandler 
    : IRequestHandler<UploadDueListCommand, ResponseUploadDueListCommand>
{
    private readonly IFileService _fileService;

    public UploadDueListCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }
    
    public async Task<ResponseUploadDueListCommand>
        Handle(UploadDueListCommand request, CancellationToken cancellationToken)
    {
        var uploadFile = request.UploadFileCommand;
        var res
            = await _fileService.UploadFile(
                new UploadFileRequest()
                {
                    File = uploadFile.File,
                    Description = uploadFile.Description,
                    UploadedBy = uploadFile.UploadedBy
                });

        return new ResponseUploadDueListCommand()
        {
            Status = res.Status,
            InsertedId = res.InsertedId
        };
    }
}