using backEnd.Models;
using Microsoft.AspNetCore.Http;
using SaasKit.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backEnd.Infra
{
    

    public class AppTenantResolver : ITenantResolver<AppTenant>
    {
        private BackEndContext db;
        public AppTenantResolver(BackEndContext backEndContext)
        {
            db = backEndContext;
        }

        public async Task<TenantContext<AppTenant>> ResolveAsync(HttpContext context)
        {
            TenantContext<AppTenant> tenantContext = null;
            Console.WriteLine("-->" + context.Request.Host.ToString());
            var tenant = db.Tenants.FirstOrDefault(t =>
                t.Hostnames.Any(h => h.Equals(context.Request.Host.Value.ToLower())));
           
            if (tenant != null)
            {
                Console.WriteLine(tenant.Name + ":" + tenant.Hostnames[0]);
                tenantContext = new TenantContext<AppTenant>(tenant);
            }
            else
            {
                tenant = new AppTenant {Id=0, Hostname =  "34.73.160.251" ,Name="ADMIN TENANT"};
                Console.WriteLine(tenant.Name + ":" + tenant.Hostnames[0]);
                tenantContext = new TenantContext<AppTenant>(tenant);
            }
            
            Console.WriteLine(context.Request.Path);
            return tenantContext; 
        }
    }
}
