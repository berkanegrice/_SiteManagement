using SiteManagement.MVC.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace SiteManagement.MVC.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}