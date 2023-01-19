using System.Reflection;
using AutoMapper;
using SiteManagement.Application.Common.Helper;
using SiteManagement.Application.DueRelated.DueInformations.Queries.GetDueInformations;
using SiteManagement.Application.Files.Queries.GetFiles;
using SiteManagement.Domain.Entities.DuesRelated;
using SiteManagement.Domain.Entities.FileRelated;

namespace SiteManagement.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Mapps
        
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        CreateMap<DueInformation, DueInformationDto>()
            .ForMember(
                dto => dto.LeaseHolder,
                conf =>
                    conf.MapFrom(ol => ol.User.UserName))
            .ForMember(dto => dto.Email,
                conf =>
                    conf.MapFrom(ol => ol.User.Email))
            .ForMember(dto => dto.Debt,
                conf =>
                    conf.MapFrom(ol => ol.Debt.ToDouble()))
            .ForMember(dto => dto.Credit,
                conf =>
                    conf.MapFrom(ol => ol.Credit.ToDouble()))
            .ForMember(dto => dto.BalanceDebt,
                conf =>
                    conf.MapFrom(ol => ol.BalanceDebt.ToDouble()))
            .ForMember(dto => dto.BalanceCredit,
                conf =>
                    conf.MapFrom(ol => ol.BalanceCredit.ToDouble()));
       
        
        //TODO: This should be refactored.
        CreateMap<FileOnDatabaseModel, FileOnDataBaseDto>()
            .ForMember(dto => dto.Id,
                conf =>
                    conf.MapFrom(ol => ol.Id))
            .ForMember(dto => dto.Name,
                conf =>
                    conf.MapFrom(ol => ol.Name))
            .ForMember(dto => dto.FileType,
                conf =>
                    conf.MapFrom(ol => ol.FileType))
            .ForMember(dto => dto.Extension,
                conf =>
                    conf.MapFrom(ol => ol.Extension))
            .ForMember(dto => dto.Description,
                conf =>
                    conf.MapFrom(ol => ol.Description))
            .ForMember(dto => dto.UploadedBy,
                conf =>
                    conf.MapFrom(ol => ol.UploadedBy))
            .ForMember(dto => dto.CreatedOn,
                conf =>
                    conf.MapFrom(ol => ol.CreatedOn))
            .ForMember(dto => dto.Data,
                conf =>
                    conf.MapFrom(ol => ol.Data));

        // TODO: Fix Automapper for DueTransaction.
        // CreateMap<DueTransaction, DueTransactionDto>()
        //     .IncludeBase<DueInformation, DueInformationDto>();
        
        #endregion
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);
        
        var mappingMethodName = nameof(IMapFrom<object>.Mapping);
    
        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        
        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();
        
        var argumentTypes = new Type[] { typeof(Profile) };
    
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            
            var methodInfo = type.GetMethod(mappingMethodName);
    
            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();
    
                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);
    
                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}