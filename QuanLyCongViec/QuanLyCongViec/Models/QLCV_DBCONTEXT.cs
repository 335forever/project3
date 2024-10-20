using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace QuanLyCongViec.Models
{
    public class QLCV_DBCONTEXT : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Resource> Resources { set; get; }
        public DbSet<Role_Permission> Roles_Permission { set; get; }
        public DbSet<Order> Orders { set; get; }

        private readonly IConfiguration _configuration;

        public QLCV_DBCONTEXT(DbContextOptions<QLCV_DBCONTEXT> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role_Permission>()
                 .HasKey(rp => new { rp.role_id, rp.resource_id });

            modelBuilder.Entity<Role_Permission>()
                .HasOne(rp => rp.Role)
                .WithMany(role => role.Role_Permissions)
                .HasForeignKey(rp => rp.role_id);

            modelBuilder.Entity<Role_Permission>()
                .HasOne(rp => rp.Resource)
                .WithMany(resource => resource.Role_Permissions)
                .HasForeignKey(rp => rp.resource_id);
        }
    }
}
