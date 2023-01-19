using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SiteManagement.Application.Common.Interfaces;
using SiteManagement.Application.Common.Models;
using SiteManagement.Application.Common.Models.DueRelated;
using SiteManagement.Application.DueRelated.DueInformations.Command;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;
using SiteManagement.Domain.Entities.DuesRelated;
using SiteManagement.Infrastructure.Services.CsvReaderHelper;

namespace SiteManagement.Infrastructure.Services.Dues;

public class DueFactory : IDueFactory
{
    private readonly IApplicationDbContext _context;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    public DueFactory(IApplicationDbContext context, IFileService fileService, IMapper mapper)
    {
        _context = context;
        _fileService = fileService;
        _mapper = mapper;
    }
    
    private static List<string> Cleaner(string inputDueData)
    {
        inputDueData = inputDueData
            .Replace(";;;;;;;;;", "")
            .Replace(";;Nakli Yekün;;;;  ;  ;  ;", "")
            .Replace("TARİH", "Date")
            .Replace("FİŞ NO", "FisNo")
            .Replace(";A Ç I K L A M A", "Detail")
            .Replace(";;BORÇ (TL)", "Debt")
            .Replace("ALACAK (TL)", "Credit")
            .Replace("BORÇ BAKİYE (TL)", "BalanceDebt")
            .Replace("ALACAK BAKİYE (TL)", "BalanceCredit")
            .Replace(";;;;", ";")
            .Replace(";;", ";")
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
    
    private static List<DueTransaction> Process(string rawDueData)
    {
        var cleaned = Cleaner(rawDueData);
        var accountCode = cleaned[0]
            .Split(";")[1];

        return cleaned.Skip(2)
            .Select(dueInfo => dueInfo.Split(";"))
            .Select(dueInfoParts => new DueTransaction()
            {
                AccountCode = int.Parse(accountCode.Replace(" ", "").Trim()),
                Date = DateTime.ParseExact(dueInfoParts[0], "dd.MM.yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.AdjustToUniversal),
                Detail = dueInfoParts[2],
                Debt = string.IsNullOrWhiteSpace(dueInfoParts[3]) ? "" : dueInfoParts[3],
                Credit = string.IsNullOrWhiteSpace(dueInfoParts[4]) ? "" : dueInfoParts[4],
                BalanceDebt = string.IsNullOrWhiteSpace(dueInfoParts[5]) ? "" : dueInfoParts[5],
                BalanceCredit = string.IsNullOrWhiteSpace(dueInfoParts[6]) ? "" : dueInfoParts[6]
            })
            .ToList();
    }
    
    public async Task<ResponseApplyDueListCommand> ApplyDueInfList(ApplyDueListRequest applyUserListRequest)
    {
        #region Fetch Data

        var newDueListDto = await _fileService
            .FetchFileById(new FetchFileRequest()
            {
                Id = applyUserListRequest.Id
            });
        
        #endregion

        #region Remove Old Data

        _context.DueInformations.RemoveRange(_context.DueInformations);

        #endregion
        
        #region Process Data

        var duesOnCsv = Serializer<DueOnCsv>
            .Deserialize(newDueListDto.Data)
            .SkipWhile(x => x.AccountCode != "HESAP KODU")
            .Skip(2)
            .Where(x => x.AccountCode is { Length: > 6 });
        
        #endregion
        
        #region Add Data
    
        foreach (var dueOnCsv in duesOnCsv)
        {
            var dueModel = new DueModel(dueOnCsv);
            _context.DueInformations.Add(new DueInformation()
            {
                AccountCode = dueModel.AccountCode,
                Credit = dueModel.Credit,
                Debt = dueModel.Debt,
                BalanceDebt = dueModel.BalanceDebt,
                BalanceCredit = dueModel.BalanceCredit
            });
        }
        
        #endregion

        #region Return Response

        return new ResponseApplyDueListCommand()
        {
            Status = await _context.SaveChangesAsync(default) > 0,
            Type = "Mizan",
        };
        
        #endregion
    }

    public async Task<ResponseApplyDueListCommand> ApplyDueTransList(ApplyDueListRequest applyUserListRequest)
    {
        #region Fetch Data

        var newDueListDto = await _fileService
            .FetchFileById(new FetchFileRequest()
            {
                Id = applyUserListRequest.Id
            });
        
        #endregion
        
        #region Remove Old Data

        _context.DueTransactions.RemoveRange(_context.DueTransactions);

        #endregion

        #region Process Data/w Add Data
        
        using var ms = new MemoryStream(newDueListDto.Data);
        using var sr = new StreamReader(ms);
        var duePart = "";
        while(await sr.ReadLineAsync() is { } currentLine)
        {
            if (!currentLine.Contains("T O P L A M"))
            {
                duePart = duePart + currentLine + "\n";
            }
            else
            {
                var dues = Process(duePart);
                dues.ForEach(f => _context.DueTransactions.Add(f));
                duePart = "";
            }
        }
        
        #endregion
        
        #region Return Response

        return new ResponseApplyDueListCommand()
        {
            Status = await _context.SaveChangesAsync(default) > 0,
            Type = "Muavin",
        };
        
        #endregion
    }

    public async Task<IQueryable<DueInformationDto>> GetAllDueInformation()
    {
        return await Task.FromResult(_context.DueInformations.ProjectTo<DueInformationDto>
            (_mapper.ConfigurationProvider));
    }
}