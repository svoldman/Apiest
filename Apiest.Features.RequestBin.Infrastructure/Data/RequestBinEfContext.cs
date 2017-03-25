using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.RequestBin.Infrastructure.Data.Models;

namespace Apiest.Features.RequestBin.Infrastructure.Data
{
    public class RequestBinEfContext : DbContext
    {
        public RequestBinEfContext() : base("name=ApiHooks")
        {
        }

        public RequestBinEfContext(string connectionString) 
            : base(connectionString)
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<WebRequest>().HasKey<int>(p => p.WebRequestId);
            modelBuilder.Entity<WebRequest>().Property(p => p.WebRequestId).HasColumnName("Id");
            modelBuilder.Entity<WebRequest>().Map(m => m.ToTable("WebRequest"));
            modelBuilder.Entity<WebRequest>()
                .HasRequired<WebRequestGroup>(p => p.WebRequestGroup)
                .WithMany(p => p.WebRequests)
                .HasForeignKey(s=>s.WebRequestGroupId);

            modelBuilder.Entity<WebRequestGroup>().HasKey<int>(p => p.WebRequestGroupId);
            modelBuilder.Entity<WebRequestGroup>().Property(p => p.WebRequestGroupId).HasColumnName("Id");
            modelBuilder.Entity<WebResponse>().HasKey<int>(p => p.WebResponseId);
            modelBuilder.Entity<WebResponse>().Property(p => p.WebResponseId).HasColumnName("Id");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<WebRequest> WebRequests { get; set; }
        public DbSet<WebRequestGroup> WebRequestGroups { get; set; }
        public DbSet<WebResponse> WebResponse { get; set; }
    }
}
