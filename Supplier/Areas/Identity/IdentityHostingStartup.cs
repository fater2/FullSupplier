using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Supplier.Areas.Identity.Data;
using Supplier.Data;

[assembly: HostingStartup(typeof(Supplier.Areas.Identity.IdentityHostingStartup))]
namespace Supplier.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SupplierIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SupplierDbContextConnection")));

                services.AddDefaultIdentity<Admin>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<SupplierIdentityDbContext>();
            });
        }
    }
}