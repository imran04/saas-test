using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SaasKit.Multitenancy;

namespace backEnd.Models
{
    public class TeanantContext : DbContext
    {
        private  AppTenant tenant;
        public TeanantContext(DbContextOptions<TeanantContext> options,
                            TenantContext<AppTenant> tenantProvider)
        {
            if(tenantProvider!=null)
           { this.tenant = tenantProvider.Tenant;
            Database.EnsureCreated();
           }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(tenant==null){
                tenant= new AppTenant{Name="default"};
            }
            var connectionString = $"server=3.17.110.191;database={tenant.Name.Replace(' ', '_')};user id=root1;password=1qaz2wsx";
            optionsBuilder.UseMySQL(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<backEnd.Models.FormInfo> FormInfo { get; set; }
    }
   
}
