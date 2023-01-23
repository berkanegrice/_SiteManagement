using System.ComponentModel.DataAnnotations;
using SiteManagement.Domain.Entities.Enums;

namespace SiteManagement.MVC.Models;

public class UploadRegisterViewModel
{
    [EnumDataType(typeof(RegisterName))]
    public RegisterName RegisterName { get; set; }
    
    [EnumDataType(typeof(RegisterType))]
    public RegisterType RegisterType { get; set; }
}