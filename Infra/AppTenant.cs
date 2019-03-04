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
    }

    public class AppTenantResolver : ITenantResolver<AppTenant>
    {
        IEnumerable<AppTenant> tenants = new List<AppTenant>(new[]
        {
        new AppTenant {
            Name = "Tenant 1",
            Hostnames = new[] { "34.73.160.251/aa" }
        },
         new AppTenant {
            Name = "yusuf",
            Hostnames = new[] { "34.73.160.251/c/" }
        },
        new AppTenant {
            Name = "Tenant 2",
            Hostnames = new[] { "34.73.160.251" }
        }
    });

        public async Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<AppTenant> tenantContext = null;
            Console.WriteLine("-->" + context.Request.Host.Value.ToLower());
            var tenant = tenants.FirstOrDefault(t =>
                t.Hostnames.Any(h => h.Equals(context.Request.Host.Value.ToLower())));

            if (tenant != null)
            {
                Console.WriteLine(tenant.Name + ":" + tenant.Hostnames);
                tenantContext = new TenantContext<AppTenant>(tenant);
            }
            else
            {
                tenant = tenants.FirstOrDefault();
                Console.WriteLine(tenant.Name + ":" + tenant.Hostnames);
                tenantContext = new TenantContext<AppTenant>(tenants.FirstOrDefault());
            }

            return tenantContext; 
        }
    }
}
