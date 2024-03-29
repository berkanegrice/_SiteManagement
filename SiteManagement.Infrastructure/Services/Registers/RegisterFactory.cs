using System.Globalization;
using System.Text.RegularExpressions;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Interfaces.Due;
using SiteManagement.Application.Common.Models.Requests.File;
using SiteManagement.Application.Common.Models.Requests.Register;
using SiteManagement.Application.DueRelated.DueInformations.Response;
using SiteManagement.Infrastructure.Services.CsvReaderHelper;
using SiteManagement.Application.Common.Helper;
using SiteManagement.Domain.Entities.RegisterRelated;
using AutoMapper;
using AutoMapper.Internal;
using SiteManagement.Application.RegisterRelated.RegisterInformations.Response;

namespace SiteManagement.Infrastructure.Services.Registers;

public class RegisterFactory : IRegisterFactory
{
    private readonly IApplicationDbContext _context;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    public RegisterFactory(
        IApplicationDbContext context, IFileService fileService, IMapper mapper)
    {
        _context = context;
        _fileService = fileService;
        _mapper = mapper;
    }
    
    private static List<string> Cleaner(string inputDueData)
    {
        inputDueData = inputDueData.Replace("	", ";"); 
        inputDueData = inputDueData
            .Replace(";;;;;;;;;", "")
            .Replace(";;Nakli Yekün;;;;;;;", "")
            .Replace(";;Nakli Yekün;;;;  ;  ;  ;", "")
            .Replace("TARİH", "Date")
            .Replace("FİŞ NO", "FisNo")
            .Replace(";A Ç I K L A M A", "Detail")
            .Replace(";;BORÇ (TL)", "Debt")
            .Replace("ALACAK (TL)", "Credit")
            .Replace("BORÇ BAKİYE (TL)", "BalanceDebt")
            .Replace("ALACAK BAKİYE (TL)", "BalanceCredit")
            .Replace(";;;;", ";")
            .Replace("  ;", ";")
            .Replace(";  ;", ";;");

        var cleaned = Regex
            .Replace(inputDueData, @"^\s*$[\r\n]*", string.Empty, RegexOptions.Multiline)
            .Split("\n")
            .ToList();
        
        cleaned.RemoveAt(0);
        cleaned.RemoveAt(cleaned.Count-1);

        return cleaned;
    }
    private static List<RegisterTransaction> Process(string rawDueData, string type)
    {
        var cleaned = Cleaner(rawDueData);
        var accountCode = cleaned[0]
            .Split(";;")[1];

        return cleaned.Skip(2)
            .Select(dueInfo => dueInfo.Split(";"))
            .Select(dueInfoParts => new RegisterTransaction()
            {
                AccountCode = int.Parse(accountCode.Replace(" ", "").Trim()),
                Type = type,
                Date = DateTime.ParseExact(
                    dueInfoParts[0], "dd.MM.yyyy", 
                    CultureInfo.InstalledUICulture, DateTimeStyles.AdjustToUniversal),
                Detail = dueInfoParts[2],
                Debt = (string.IsNullOrWhiteSpace(dueInfoParts[3]) ? "" : dueInfoParts[3]).ToDouble(),
                Credit = (string.IsNullOrWhiteSpace(dueInfoParts[4]) ? "" : dueInfoParts[4]).ToDouble(),
                BalanceDebt = (string.IsNullOrWhiteSpace(dueInfoParts[5]) ? "" : dueInfoParts[5]).ToDouble(),
                BalanceCredit = (string.IsNullOrWhiteSpace(dueInfoParts[6]) ? "" : dueInfoParts[6]).ToDouble()
            })
            .ToList();
    }
    
    public async Task<ResponseApplyRegisterCommand>
        ApplyRegisterInfList(ApplyRegisterRequest request)
    {
        #region Fetch Data
        
        var newRegisterInfDto = await _fileService
            .FetchFileById(new FetchFileRequest()
            {
                Id = request.Id
            });
        
        #endregion
        
        #region Remove Old Data
        
        var removeCond = request.Name switch
        {
            "Aidat" => 13101000,
            "Sufa" => 13201000,
            "Kidem" => 13301000,
            _ => throw new NotImplementedException()
        };
        
        var toRemove = _context.RegisterInformations
            .Where(x => x.Type.Equals(request.Name));
        toRemove.ForAll(f => _context.RegisterInformations.Remove(f));
        
        await _context.SaveChangesAsync(default);

        #endregion
        
        #region Process Data
        
        var registersInfOnCsv = Serializer<RegisterInfOnCsv>
            .Deserialize(newRegisterInfDto.Data)
            .SkipWhile(x => x.AccountCode != "HESAP KODU")
            .Skip(2)
            .Where(x => x.AccountCode is { Length: > 6 });
        
        #endregion
        
        #region Add Data
        
        foreach (var registerInfOnCsv in registersInfOnCsv)
        {
            var registerInfModel = new RegisterInfModel(registerInfOnCsv);
            _context.RegisterInformations.Add(new RegisterInformation()
            {
                AccountCode = registerInfModel.AccountCode,
                Type = request.Name,
                Credit = registerInfModel.Credit,
                Debt = registerInfModel.Debt,
                BalanceDebt = registerInfModel.BalanceDebt,
                BalanceCredit = registerInfModel.BalanceCredit
            });
        }

        #endregion
        
        #region Return Response
        
        return new ResponseApplyRegisterCommand()
        {
            Status = await _context.SaveChangesAsync(default) > 0,
            Type = $"{request.Name} {"Mizan"} yuklendi",
        };
        
        #endregion
    }

    public async Task<ResponseApplyRegisterCommand> 
        ApplyRegisterTransList(ApplyRegisterRequest request)
    {
        #region Fetch Data
        
        var newRegisterTransDto = await _fileService
            .FetchFileById(new FetchFileRequest()
            {
                Id = request.Id
            });
        
        #endregion
        
        #region Remove Old Data
        
        var removeCond = request.Name switch
        {
            "Aidat" => 13101000,
            "Kidem" => 13301000,
            "Sufa" => 13201000,
            _ => throw new NotImplementedException()
        };
        
        var toRemove = _context.RegisterTransactions
            .Where(x => x.Type.Equals(request.Name));
        toRemove.ForAll(f => _context.RegisterTransactions.Remove(f));
        
        #endregion
        
        #region Process Data/w Add Data
        
        using var ms = new MemoryStream(newRegisterTransDto.Data);
        using var sr = new StreamReader(ms);
        var part = "";
        while(await sr.ReadLineAsync() is { } currentLine)
        {
            if (!currentLine.Contains("T O P L A M"))
            {
                part = part + currentLine + "\n";
            }
            else
            {
                var register = Process(part, request.Name);
                
                register.ForEach(f => _context.RegisterTransactions.Add(f));
                part = "";
            }
        }
        
        #endregion
        
        #region Return Response
        
        return new ResponseApplyRegisterCommand()
        {
            Status = await _context.SaveChangesAsync(default) > 0,
            Type = $"{request.Name} {"Muavin"} yuklendi",
        };
        
        #endregion
    }
}