using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyCongViec.Models.Core;
using System;

namespace QuanLyCongViec.Models
{
    public class QLCV_DBCONTEXT : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Resource> Resources { set; get; }
        public DbSet<Role_Permission> Roles_Permission { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<Order_Permission> Order_Permission { set; get; }

        private readonly IConfiguration _configuration;

        public QLCV_DBCONTEXT(DbContextOptions<QLCV_DBCONTEXT> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role_Permission>()
                 .HasKey(rp => new { rp.RoleId, rp.ResourceId });

            modelBuilder.Entity<Role_Permission>()
                .HasOne(rp => rp.Role)
                .WithMany(role => role.Role_Permissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<Role_Permission>()
                .HasOne(rp => rp.Resource)
                .WithMany(resource => resource.Role_Permissions)
                .HasForeignKey(rp => rp.ResourceId);

            modelBuilder.Entity<Order_Permission>()
                .HasKey(op => new {op.OrderId, op.UserId});

            modelBuilder.Entity<Order_Permission>()
                .HasOne(op => op.Order)
                .WithMany(order => order.Order_Permissions)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<Order_Permission>()
                .HasOne(op => op.User)
                .WithMany(user => user.Order_Permissions)
                .HasForeignKey(op => op.UserId);
        }
    }
}
