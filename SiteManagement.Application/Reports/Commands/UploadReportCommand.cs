using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFiles;

namespace SiteManagement.Application.Reports.Commands;

public record UploadReportCommand
    : IRequest<ResponseUploadFileCommand>
{
    public UploadFileCommand UploadFileCommand { get; set; }
}

public class UploadNewsCommandHandler
    : IRequestHandler<UploadReportCommand, ResponseUploadFileCommand>
{
    private readonly IFileService _fileService;

    public UploadNewsCommandHandler(IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task<ResponseUploadFileCommand>
        Handle(UploadReportCommand request, CancellationToken cancellationToken)
    {
        var uploadFile = request.UploadFileCommand;
        var res
            = await _fileService.UploadFile(
                new UploadFileRequest()
                {
                    FormFile = uploadFile.File,
                    Description = uploadFile.Description,
                    UploadedBy = uploadFile.UploadedBy
                });

        return new ResponseUploadFileCommand()
        {
            Success = res.Success,
            InsertedId = res.InsertedId
        };
    }
}