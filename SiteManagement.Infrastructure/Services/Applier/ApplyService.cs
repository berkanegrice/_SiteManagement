using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models.Requests.File;
using SiteManagement.Application.Common.Models.Requests.Register;
using SiteManagement.Application.DueRelated.DueInformations.Command;
using SiteManagement.Application.DueRelated.DueInformations.Response;
using SiteManagement.Application.Files.Response;
using SiteManagement.Domain.Entities;

namespace SiteManagement.Infrastructure.Services.Applier;

public class ApplyService : IApplyService
{
    private readonly IDueFactory _dueFactory;
    private readonly IFileService _fileService;

    public ApplyService(IDueFactory dueFactory, IFileService fileService)
    {
        _dueFactory = dueFactory;
        _fileService = fileService;
    }
    
    public async Task<ResponseApplyRegisterCommand> ApplyList(int requestId)
    {
        var toApply =
            await _fileService.FetchFileById(new FetchFileRequest()
        {
            Id = requestId
        });

        var register = new Register(toApply.FileType.Split(" "));

        return register.RegisterType switch
        {
            "Mizan" => await _dueFactory.ApplyDueInfList(new ApplyRegisterRequest() {Id = requestId}),
            "Muavin" => await _dueFactory.ApplyDueTransList(new ApplyRegisterRequest() {Id = requestId}),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}