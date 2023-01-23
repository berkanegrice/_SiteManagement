using MediatR;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Files.Response;

namespace SiteManagement.Application.Files.Commands.ApplyFiles;

public record ApplyListCommand
    : IRequest<ResponseApplyListCommand>
{
    public int Id { get; init; }
}

public class ApplyListCommandHandler
    : IRequestHandler<ApplyListCommand, ResponseApplyListCommand>
{
    private readonly IApplyService _applyService;

    public ApplyListCommandHandler(IApplyService applyService)
    {
        _applyService = applyService;
    }
    
    public async Task<ResponseApplyListCommand>
        Handle(ApplyListCommand request, CancellationToken cancellationToken)
    {
        var resp = await _applyService.ApplyList(request.Id);
        return new ResponseApplyListCommand()
        {
            Status = resp.Status,
            InsertedId = resp.InsertedId,
            Type = resp.Type
        };
    }
}