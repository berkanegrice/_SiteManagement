using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Files.Commands.UploadFiles;

namespace SiteManagement.Application.Users.Commands;

public record UploadUserListCommand
    : IRequest<ResponseUploadUserListCommand>
{
    public UploadFileCommand UploadFileCommand { get; set; }
}

public class UploadUserListCommandHandler
    : IRequestHandler<UploadUserListCommand, ResponseUploadUserListCommand>
{

    private readonly IUserFactory _userFactory;

    public UploadUserListCommandHandler(IUserFactory userFactory)
    {
        _userFactory = userFactory;
    }
    
    public async Task<ResponseUploadUserListCommand> Handle(
        UploadUserListCommand request, CancellationToken cancellationToken)
    {
        return await _userFactory.UploadUserList(new UploadFileRequest()
        {
            FormFile = request.UploadFileCommand.File,
            Description = request.UploadFileCommand.Description,
            UploadedBy = request.UploadFileCommand.UploadedBy
        });

        // var res = await _mediator.Send(new UploadFileCommand()
        // {
        //     File = request.File,
        //     Description = request.Description,
        //     UploadedBy = request.UploadedBy
        //
        // }, cancellationToken);
        //
        // return new ResponseUploadUserListCommand()
        // {
        //     Success = res.Success,
        //     InsertedId = res.InsertedId
        // };
    }
} 