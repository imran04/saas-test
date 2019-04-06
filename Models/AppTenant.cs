using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backEnd.Models
{
    public class AppTenant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Hostname { get; set; }

        public string ConnectionString { get; set; }

        [NotMapped]
        public string[] Hostnames => Hostname.Split('|');
        //public string BasePath { get; set; }
        
    }

    public class BackEndContext : DbContext
    {
        public BackEndContext(DbContextOptions<BackEndContext> options)
            : base(options)
        { }

        public DbSet<AppTenant> Tenants { get; set; }
       
    }
}
