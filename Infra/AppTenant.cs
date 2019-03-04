using Microsoft.AspNetCore.Http;
using SaasKit.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backEnd.Infra
{
    public class AppTenant
    {
        public string Name { get; set; }
        public string[] Hostnames { get; set; }
        //public string BasePath { get; set; }
    }

    public class AppTenantResolver : ITenantResolver<AppTenant>
    {
        IEnumerable<AppTenant> tenants = new List<AppTenant>(new[]
        {
        new AppTenant {
            Name = "Tenant 1",
            Hostnames = new[] { "34.73.160.251" },   
        },
         new AppTenant {
            Name = "goldpi",
            Hostnames = new[] { "test.goldpi.com" },
           
        },
         new AppTenant {
            Name = "Home Teant",
            Hostnames = new[] { "34.73.160.251" },
           
            
        },
        new AppTenant {
            Name = "Tenant 2",
            Hostnames = new[] { "34.73.160.251" },
         
        }
    });

        public async Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<AppTenant> tenantContext = null;
            Console.WriteLine("-->" + context.Request.Host.ToString());
            var tenant = tenants.FirstOrDefault(t =>
                t.Hostnames.Any(h => h.Equals(context.Request.Host.Value.ToLower())));
           
            if (tenant != null)
            {
                Console.WriteLine(tenant.Name + ":" + tenant.Hostnames[0]);
                tenantContext = new TenantContext<AppTenant>(tenant);
            }
            else
            {
                tenant = tenants.FirstOrDefault();
                Console.WriteLine(tenant.Name + ":" + tenant.Hostnames[0]);
                tenantContext = new TenantContext<AppTenant>(tenant);
            }
            // Console.WriteLine(context.Request.Path.ToString().Remove(0, tenant.BasePath.Length));
            //context.Request.Path=context.Request.Path.ToString().Remove(0, tenant.BasePath.Length);
            Console.WriteLine(context.Request.Path);
            return tenantContext; 
        }
    }
}
