using Microsoft.EntityFrameworkCore;
using SiteManagement.MVC.Models;
using System.Linq.Dynamic.Core;

namespace SiteManagement.MVC.Services;

public abstract class DataTableService<T> 
{
    private IFormCollection FormCollection { get; set; }

    protected DataTableService(IFormCollection formCollection)
    {
        FormCollection = formCollection;
    }
   
    public async Task<object> ServerSideSorting(IQueryable<T> value)
    {
        #region DataTable Params
        
        var dataTableConf = new DataTableVm
        (
            FormCollection["draw"].FirstOrDefault(),
            FormCollection["start"].FirstOrDefault(),
            FormCollection["length"].FirstOrDefault(),
            FormCollection["columns[" + FormCollection["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(),
            FormCollection["order[0][dir]"].FirstOrDefault(),
            FormCollection["search[value]"].FirstOrDefault(),
            FormCollection["length"].FirstOrDefault() != null ? Convert.ToInt32(FormCollection["length"].FirstOrDefault()) : 0,
            FormCollection["start"].FirstOrDefault() != null ? Convert.ToInt32(FormCollection["start"].FirstOrDefault()) : 0
        );
        
        #endregion

        if (!(string.IsNullOrEmpty(dataTableConf.SortColumn) && string.IsNullOrEmpty(dataTableConf.SortColumnDirection)))
            value = value.OrderBy(dataTableConf.SortColumn + " " + dataTableConf.SortColumnDirection);
        
        if (!string.IsNullOrEmpty(dataTableConf.SearchValue))
            value = Searcher(value, dataTableConf.SearchValue);
        
        var data = await value.Skip(dataTableConf.Skip).Take(dataTableConf.PageSize).ToListAsync();
        return 
            new {  dataTableConf.Draw, recordsFiltered = value.Count(), recordsTotal = value.Count(), data };
    }
    
    protected abstract IQueryable<T> Searcher(IQueryable<T> value, string searchValue);
}