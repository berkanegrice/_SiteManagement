namespace SiteManagement.MVC.Models;

public class UploadRegisterViewModel
{
    public UploadRegisterViewModel()  
    {  
        UploadRegisterList = new List<Itemlist>() {  
            new Itemlist { Text = "Khushbu", Value = 1 },  
            new Itemlist { Text = "Mohan", Value = 2 },  
            new Itemlist { Text = "John", Value = 3 },  
            new Itemlist { Text = "Martin", Value = 4 },  
            new Itemlist { Text = "Due", Value = 5 },  
        };  
    }  
    public int EmployeeId { get; set; }  
  
    public List<Itemlist> UploadRegisterList { get; set; }  
}

public class Itemlist  
{  
    public string Text { get; set; }  
    public int Value { get; set; }  
}  