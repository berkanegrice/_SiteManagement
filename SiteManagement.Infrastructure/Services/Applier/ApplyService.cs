using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Interfaces.Due;
using SiteManagement.Application.Common.Interfaces.User;
using SiteManagement.Application.Common.Models.Requests.File;
using SiteManagement.Application.Common.Models.Requests.Register;
using SiteManagement.Application.DueRelated.DueInformations.Response;
using SiteManagement.Domain.Entities;

namespace SiteManagement.Infrastructure.Services.Applier;

public class ApplyService : IApplyService
{
    private readonly IUserFactory _userFactory;
    private readonly IRegisterFactory _registerFactory;
    private readonly IFileService _fileService;

    public ApplyService(IRegisterFactory registerFactory, IFileService fileService, IUserFactory userFactory)
    {
        _registerFactory = registerFactory;
        _fileService = fileService;
        _userFactory = userFactory;
    }
    
    public async Task<ResponseApplyRegisterCommand> ApplyList(int requestId)
    {
        var data =
            await _fileService.FetchFileById(new FetchFileRequest()
        {
            Id = requestId
        });

        var register = new Register(data.FileType.Split(" "));

        return register.RegisterType switch
        {
            "Mizan" => await _registerFactory.ApplyRegisterInfList(new ApplyRegisterRequest() {Id = requestId, Name = register.RegisterName}),
            "Muavin" => await _registerFactory.ApplyRegisterTransList(new ApplyRegisterRequest() {Id = requestId, Name = register.RegisterName}),
            _ => throw new NotImplementedException()
        };
    }
}