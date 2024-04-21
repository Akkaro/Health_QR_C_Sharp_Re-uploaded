using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Health_QR.Migrations
{
    public partial class SeedRoles : Migration
    {
        private string DoctorRoleId = Guid.NewGuid().ToString();
        private string NurseRoleId = Guid.NewGuid().ToString();
        private string AdminRoleId = Guid.NewGuid().ToString();

        private string User1Id = Guid.NewGuid().ToString();
        private string User2Id = Guid.NewGuid().ToString();

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);

            //SeedUser(migrationBuilder);

            //SeedUserRoles(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AdminRoleId}', 'Administrator', 'ADMINISTRATOR', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{DoctorRoleId}', 'Doctor', 'DOCTOR', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{NurseRoleId}', 'Nurse', 'NURSE', null);");
        }

        /*
        private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('de091935-d6b1-4c37-a7cf-1a582468fbfa', '{AdminRoleId}');
        INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
        VALUES
           ('de091935-d6b1-4c37-a7cf-1a582468fbfa', '{DoctorRoleId}');");

        }
        */
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}