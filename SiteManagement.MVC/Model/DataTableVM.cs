namespace SiteManagement.MVC.Model;

public class DataTableVm
{
    public DataTableVm(string? draw, string? start, string? length, string? sortColumn, string? sortColumnDirection, string? searchValue, int pageSize, int skip)
    {
        Draw = draw;
        Start = start;
        Length = length;
        SortColumn = sortColumn;
        SortColumnDirection = sortColumnDirection;
        SearchValue = searchValue;
        PageSize = pageSize;
        Skip = skip;
    }

    public string? Draw { get; set; }
    public  string? Start { get; set; }
    public  string? Length { get; set; }
    public  string? SortColumn { get; set; }
    public  string? SortColumnDirection { get; set; }
    public  string? SearchValue { get; set; }
    public  int PageSize { get; set; }
    public  int Skip { get; set; }
}