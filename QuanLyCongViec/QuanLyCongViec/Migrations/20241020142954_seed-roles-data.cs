using Microsoft.EntityFrameworkCore.Migrations;
using QuanLyCongViec.SeedData;

#nullable disable

namespace QuanLyCongViec.Migrations
{
    public partial class seedrolesdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var managerRoleId = RoleSeed.managerRoleId;
            var designerRoleId = RoleSeed.designerRoleId;
            var producerRoleId = RoleSeed.producerRoleId;
            var salerRoleId = RoleSeed.salerRoleId;

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "role_id", "role_name" },
                values: new object[,]
                {
                    { managerRoleId, "Manager" },
                    { designerRoleId, "Designer" },
                    { producerRoleId, "Producer" },
                    { salerRoleId, "Saler" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
